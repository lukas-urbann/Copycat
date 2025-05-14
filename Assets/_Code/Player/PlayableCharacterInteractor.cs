using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

namespace BachelorProject.Player
{
    [RequireComponent(typeof(PlayableCharacterInput))]
    public class PlayableCharacterInteractor : MonoBehaviour
    {
        [SerializeField] private FloatReference interactionDistance;
        [Tooltip("Která maska se považuje za interaktovatelnou")]
        [SerializeField] private LayerMask interactableLayer = ~0; // Výchozí na 0, aby bylo zahrnuto vše
        [SerializeField] private GameObject interactionPrompt;
        [SerializeField] private Camera interactionCamera;

        [Header("Events")]
        public GameEvent InteractEvent;
        public GameEvent DropEvent;
        public GameEvent SeeInteractable;
        public GameEvent UnseeInteractable;

        #region Privátní promìnné

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
                Debug.LogError($"{typeof(PlayableCharacterInteractor)} nemá input!");
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
                    if (currentInteractable != interactable) // Pokud hráè vidí jiný interaktovatelný objekt
                    {
                        currentInteractable?.OnUnsee(); // Pokud hráè pøejel na nový objekt, tak odvolat ten starý
                        if (interactableInReach) // Pokud jsme mìli pøedtím nìjaký interactable
                        {
                            UnseeInteractable.Execute();
                        }

                        currentInteractable = interactable; // Pøesunutí na nový interactable
                        currentInteractable?.OnSee();
                        interactableInReach = true;
                        interactionPrompt?.SetActive(true);
                        SeeInteractable.Execute();
                    }
                    return;
                }
            }

            if (interactableInReach) // Pokud jsme se od interaktovatelného objektu vzdálili
            {
                currentInteractable?.OnUnsee();
                currentInteractable = null;
                interactableInReach = false;
                interactionPrompt?.SetActive(false);
                UnseeInteractable.Execute();
            }
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
            if (!interactableInReach) return;
            currentInteractable?.Interact();
        }
    }
}