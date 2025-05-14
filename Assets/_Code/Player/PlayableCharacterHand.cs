using BachelorProject.Interactable;
using UnityEngine;

namespace BachelorProject.Player
{
    public class PlayableCharacterHand : MonoBehaviour
    {
        [SerializeField] private Vector3 heldItemOffset = Vector3.zero;
        [SerializeField] private Vector3 heldItemRotation = Vector3.zero;

        [Header("Events")]
        public GameEvent ItemGrabEvent;
        public GameEvent ItemDropEvent;
        public GameEvent ItemUseEvent;

        private GameObject currentHeldItem;
        private GrabbableInteractable currentGrabbable;

        public void HoldItem(GameObject item)
        {
            if (item == null)
                return;

            if (currentHeldItem != null && currentGrabbable != null)
            {
                currentGrabbable.Drop();
            }

            currentHeldItem = item;
            currentGrabbable = item.GetComponent<GrabbableInteractable>();

            if (currentHeldItem != null && (heldItemOffset != Vector3.zero || heldItemRotation != Vector3.zero))
            {
                Transform itemTransform = currentHeldItem.transform;
                itemTransform.SetLocalPositionAndRotation(heldItemOffset, Quaternion.Euler(heldItemRotation));
            }

            ItemGrabEvent?.Execute();
        }

        public void ReleaseItem()
        {
            currentHeldItem = null;
            currentGrabbable = null;
            ItemDropEvent?.Execute();
        }

        public void UseItem()
        {
            if (currentHeldItem != null && currentGrabbable != null)
            {
                currentGrabbable.Use();
                ItemUseEvent?.Execute();
            }
        }
    }
}
