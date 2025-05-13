using UnityEngine;

namespace BachelorProject.Player
{
    public class PlayableCharacterInteractor : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [Tooltip("Key used to interact with objects")]
        [SerializeField] private KeyCode interactKey = KeyCode.E;

        [Tooltip("Maximum distance for interaction ray")]
        [SerializeField] private float interactionDistance = 3f;

        [Tooltip("Layer mask for interactable objects")]
        [SerializeField] private LayerMask interactableLayer = ~0; // Default to everything

        [Header("UI References (Optional)")]
        [Tooltip("UI element to show when looking at an interactable object")]
        [SerializeField] private GameObject interactionPrompt;

        // Private variables
        private Camera playerCamera;
        private IInteractable currentInteractable;
        private bool isLookingAtInteractable = false;

        private void Awake()
        {
            playerCamera = GetComponentInChildren<Camera>();

            if (playerCamera == null)
            {
                Debug.LogError("PlayableCharacterInteractor requires a Camera component on this object or a child object!");
            }

            // Hide interaction prompt if assigned
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }

        private void Update()
        {
            CheckForInteractable();
            HandleInteraction();
        }

        private void CheckForInteractable()
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            // Cast a ray forward to detect interactable objects
            if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
            {
                // Try to get an IInteractable component from the hit object
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
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

        private void HandleInteraction()
        {
            // If we're looking at an interactable and press the interact key
            if (isLookingAtInteractable && Input.GetKeyDown(interactKey))
            {
                if (currentInteractable != null)
                {
                    currentInteractable.Interact();
                }
            }
        }

        // Optional: Public method to allow interaction from other scripts
        public void ForceInteraction()
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }
}