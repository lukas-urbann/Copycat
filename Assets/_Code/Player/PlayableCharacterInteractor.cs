using BachelorProject.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BachelorProject.Player
{
    [RequireComponent(typeof(PlayableCharacterInput))]
    public class PlayableCharacterInteractor : MonoBehaviour
    {
        private bool canInteract = true;
        
        [SerializeField] private FloatReference interactionDistance;
        [Tooltip("Která maska se považuje za interaktovatelnou")]
        [SerializeField] private LayerMask interactableLayer = ~0; // Výchozí na 0, aby bylo zahrnuto vše
        [SerializeField] private ObjectIdentifier interactionPrompt;
        [SerializeField] private Camera interactionCamera;

        [Header("Events")]
        public VoidEvent InteractEvent;
        public VoidEvent DropEvent;
        public VoidEvent SeeInteractable;
        public VoidEvent UnseeInteractable;

        #region Priv�tn� prom�nn�

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
                Debug.LogError($"{typeof(PlayableCharacterInteractor)} nem� input!");
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
                    if (currentInteractable != interactable) // Pokud hr�� vid� jin� interaktovateln� objekt
                    {
                        currentInteractable?.OnUnsee(); // Pokud hr�� p�ejel na nov� objekt, tak odvolat ten star�
                        if (interactableInReach) UnseeInteractable.Execute(); // Pokud jsme m�li p�edt�m n�jak� interactable

                        if (interactable.IsInteractable)
                        {
                            currentInteractable = interactable; // P�esunut� na nov� interactable
                            currentInteractable?.OnSee();
                            interactableInReach = true;
                            interactionPrompt.GetSourceObject()?.SetActive(true);
                            SeeInteractable.Execute();
                        }
                    }
                    return;
                }
            }

            if (interactableInReach) // Pokud jsme se od interaktovateln�ho objektu vzd�lili
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