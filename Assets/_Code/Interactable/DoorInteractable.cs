using BachelorProject.Audio;
using BachelorProject.Management;
using BachelorProject.UI;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    public class DoorInteractable : UseableInteractable
    {
        [Header("Door Interactable")]
        [SerializeField] private bool isLocked = false;
        [SerializeField] private bool isOpen = false;
        [SerializeField] private StringReference doorKeyID;
        [SerializeField] private ObjectIdentifier playerHandIdentifier;
        [SerializeField] protected StringVariable doorLockedString;
        [SerializeField] protected StringVariable wrongKeyString;
        [SerializeField] protected StringVariable doorUnlockedString;
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private UnityEvent<bool> onDoorOpenEvent = new();
        [SerializeField] private AudioCall doorOpen;

        private void OnEnable()
        {
            UnityEventOnInteract = new UnityEvent();
            UnityEventOnInteract.AddListener(TryToggleOpen);
            UnityEventOnUse = new UnityEvent();
            UnityEventOnUse.AddListener(ToggleDoorState);
        }

        private void Start()
        {
            if (doorAnimator != null)
            {
                if (!TryGetComponent(out doorAnimator))
                {
                    Debug.Log("Dvere nemaji animator");
                }
            }

            if (doorAnimator)
                doorAnimator.SetBool("isOpen", isOpen);
        }

        private void TryToggleOpen()
        {
            InteractionText interactionText = null;
            if (ObjectRegistry.GetObject(interactionLabel, out var label)) label.TryGetComponent(out interactionText);

            if (!isLocked)
            {
                ToggleOpen();
                return;
            }

            if (!ObjectRegistry.GetObject(playerHandIdentifier, out var go))
            {
                interactionText?.SetInteractionText(doorLockedString);
                return;
            }

            var keyInteractable = go.GetComponentInChildren<KeyInteractable>();
            if (!keyInteractable)
            {
                interactionText?.SetInteractionText(doorLockedString);
                return;
            }

            if (keyInteractable.KeyID.Value != doorKeyID.Value)
            {
                interactionText?.SetInteractionText(wrongKeyString);
                return;
            }

            keyInteractable.Consume();
            interactionText?.SetInteractionText(doorUnlockedString);
            isLocked = false;
            ToggleOpen();
        }

        public void ToggleOpen()
        {
            Use();
            doorOpen.Play();
            onDoorOpenEvent?.Invoke(isOpen);
        }

        public void ToggleDoorState()
        {
            if (doorAnimator)
                doorAnimator.SetBool("isOpen", isOpen = !isOpen);
        }

        public void ToggleDoorState(bool val)
        {
            if (doorAnimator)
                doorAnimator.SetBool("isOpen", isOpen = val);
        }
    }
}
