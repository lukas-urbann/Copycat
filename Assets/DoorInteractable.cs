using UnityEngine;

namespace BachelorProject.Interactable
{
    public class DoorInteractable : UseableInteractable
    {
        [SerializeField] private bool isLocked = false;
        [SerializeField] private StringReference doorKeyID;
        [SerializeField] private ObjectIdentifier playerHandIdentifier;

        private void OnEnable()
        {
            UnityEventOnInteract.AddListener(TryOpen);
        }

        private void TryOpen()
        {
            if (!isLocked)
            {
                Open();
                return;
            }

            if (!IdentifiableObjectRegistry.GetObject(playerHandIdentifier, out var go))
            {
                return;
            }

            var keyInteractable = go.GetComponentInChildren<KeyInteractable>();
            if (!keyInteractable)
            {
                return;
            }

            if (keyInteractable.KeyID.Value != doorKeyID.Value) return;
            keyInteractable.Use();
            isLocked = false;
            Open();
        }

        private void Open()
        {
            Use();
        }
    }
}
