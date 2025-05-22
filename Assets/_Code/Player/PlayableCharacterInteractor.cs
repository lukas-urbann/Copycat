using BachelorProject.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BachelorProject.Player
{
    /// <summary>
    /// Hlavni interaktor pro hrace
    /// Pomoci raycastu scannuje pro interaktovatelne objekty
    /// </summary>
    [RequireComponent(typeof(PlayableCharacterInput))]
    public class PlayableCharacterInteractor : MonoBehaviour
    {
        private bool canInteract = true;
        
        [SerializeField] private FloatReference interactionDistance;
        [Tooltip("Která maska se považuje za interaktovatelnou")]
        [SerializeField] private LayerMask interactableLayer = ~0; // Vychozi na 0, aby bylo zahrnuto vse
        [SerializeField] private ObjectIdentifier interactionPrompt;
        [SerializeField] private Camera interactionCamera;

        [Header("Events")]
        public VoidEvent InteractEvent;
        public VoidEvent DropEvent;
        public VoidEvent SeeInteractable;
        public VoidEvent UnseeInteractable;

        #region Privatni promenne

        private IInteractable currentInteractable;
        private bool interactableInReach = false;
        [SerializeField] private PlayableCharacterInput playerInput { get; set; }

        #endregion

        private void Start()
        {
            if (TryGetComponent(out PlayableCharacterInput input))
            {
                playerInput = input;
            }
            else
            {
                Debug.LogError($"{typeof(PlayableCharacterInteractor)} nema input!");
            }

            AssignActions();
        }

        private void AssignActions()
        {
            playerInput.InteractAction.performed += PlayerInteraction;
            playerInput.DropAction.performed += PlayerDrop;
        }

        private void Update()
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            Ray ray = new(interactionCamera.transform.position, interactionCamera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance.Value, interactableLayer))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    if (currentInteractable != interactable) // Pokud hrac vidi jiny interaktovatelny objekt
                    {
                        currentInteractable?.OnUnsee(); // Pokud hrac prejel na novy, tak odhlasit stary
                        if (interactableInReach) UnseeInteractable.Execute(); // Pokud jsme meli predtim nejaky interactable

                        if (interactable.IsInteractable)
                        {
                            currentInteractable = interactable; // Presunuti na novy
                            currentInteractable?.OnSee();
                            interactableInReach = true;
                            interactionPrompt.GetSourceObject()?.SetActive(true);
                            SeeInteractable.Execute();
                        }
                    }
                    return;
                }
            }

            if (interactableInReach) // Pri vzdaleni od stareho interactablu
            {
                currentInteractable?.OnUnsee();
                currentInteractable = null;
                interactableInReach = false;
                interactionPrompt.GetSourceObject()?.SetActive(false);
                UnseeInteractable.Execute();
            }
        }

        public void SetInteractivity(bool gamePaused)
        {
            canInteract = !gamePaused;
        }

        public void PlayerInteraction(InputAction.CallbackContext ctx)
        {
            InteractEvent.Execute();
        }

        public void PlayerDrop(InputAction.CallbackContext ctx)
        {
            DropEvent.Execute();
        }

        public void HandleInteraction()
        {
            if (!canInteract) return;
            if (!interactableInReach) return;
            currentInteractable?.Interact();
        }
    }
}