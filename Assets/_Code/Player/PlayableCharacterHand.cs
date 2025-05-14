using BachelorProject.Interactable;
using UnityEngine;

namespace BachelorProject.Player
{
    public class PlayableCharacterHand : MonoBehaviour
    {
        [SerializeField] private Vector3 heldItemOffset = Vector3.zero;
        [SerializeField] private Vector3 heldItemRotation = Vector3.zero;

        private GameObject currentHeldItem;
        private GrabbableItem currentGrabbable;

        public void HoldItem(GameObject item)
        {
            if (item == null)
                return;

            if (currentHeldItem != null && currentGrabbable != null)
            {
                currentGrabbable.Drop();
            }

            currentHeldItem = item;
            currentGrabbable = item.GetComponent<GrabbableItem>();

            if (currentHeldItem != null && (heldItemOffset != Vector3.zero || heldItemRotation != Vector3.zero))
            {
                Transform itemTransform = currentHeldItem.transform;
                itemTransform.SetLocalPositionAndRotation(heldItemOffset, Quaternion.Euler(heldItemRotation));
            }
        }

        public void ReleaseItem()
        {
            currentHeldItem = null;
            currentGrabbable = null;
        }

        public void UseItem()
        {
            if (currentHeldItem != null && currentGrabbable != null)
            {
                currentGrabbable.TryConsume();
            }
        }
    }
}
