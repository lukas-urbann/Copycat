using UnityEngine;
using UnityEngine.InputSystem;

namespace BachelorProject.Player
{
    [RequireComponent(typeof(PlayableCharacterInput))]
    public class PlayableCharacterInteractor : MonoBehaviour
    {
        [SerializeField] private FloatReference interactionDistance;
        [SerializeField] private GameObject interactionPrompt;

        [Tooltip("Která maska se považuje za interaktovatelnou")]
        [SerializeField] private LayerMask interactableLayer = ~0; // Výchozí na 0, aby bylo zahrnuto vše
        [SerializeField] private PlayableCharacterInput playerInput { get; set; }

        public Camera interactionCamera;

        private IInteractable currentInteractable;
        private bool isLookingAtInteractable = false;

        private void Awake()
        {
            if (interactionCamera == null)
            {
                Debug.LogError("PlayableCharacterInteractor requires a Camera component on this object or a child object!");
            }

            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }

        private void Start()
        {
            if (TryGetComponent(out PlayableCharacterInput input))
            {
                playerInput = input;
            }
            else
            {
                Debug.LogError("Hráè nemá input!");
            }

            playerInput.InteractAction.performed += HandleInteraction;
        }

        private void Update()
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            Ray ray = new(interactionCamera.transform.position, interactionCamera.transform.forward);
            RaycastHit hit;

            // Cast a ray forward to detect interactable objects
            if (Physics.Raycast(ray, out hit, interactionDistance.Value, interactableLayer))
            {
                // Try to get an IInteractable component from the hit object
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    // If we're looking at a new interactable
                    if (currentInteractable != interactable)
                    {
                        // If we were looking at a previous interactable, call OnUnsee
                        if (currentInteractable != null)
                        {
                            currentInteractable.OnUnsee();
                        }

                        // Update current interactable and call OnSee
                        currentInteractable = interactable;
                        currentInteractable.OnSee();
                        isLookingAtInteractable = true;

                        // Show interaction prompt if assigned
                        if (interactionPrompt != null)
                        {
                            interactionPrompt.SetActive(true);
                        }
                    }

                    return;
                }
            }

            // If we reach here, we're not looking at an interactable
            if (isLookingAtInteractable)
            {
                // Call OnUnsee for previous interactable
                if (currentInteractable != null)
                {
                    currentInteractable.OnUnsee();
                }

                currentInteractable = null;
                isLookingAtInteractable = false;

                // Hide interaction prompt if assigned
                if (interactionPrompt != null)
                {
                    interactionPrompt.SetActive(false);
                }
            }
        }

        private void HandleInteraction(InputAction.CallbackContext ctx)
        {
            if (!isLookingAtInteractable) return;
            currentInteractable?.Interact();
        }

        public void ForceInteraction()
        {
            currentInteractable?.Interact();
        }
    }
}