using BachelorProject.Events;
using BachelorProject.Interactable;
using UnityEngine;

namespace BachelorProject.Player
{
    public class PlayableCharacterHand : MonoBehaviour
    {
        [SerializeField] private Vector3 heldItemOffset = Vector3.zero;
        [SerializeField] private Vector3 heldItemRotation = Vector3.zero;

        [Header("Events")]
        public VoidEvent ItemGrabEvent;
        public VoidEvent ItemDropEvent;
        public VoidEvent ItemUseEvent;

        private GameObject _currentHeldItem;
        private GrabbableInteractable _currentGrabbable;
        private bool _isUsingItem = true;

        public void ToggleHandItemUse(bool val)
        {
            _isUsingItem = val;
        }

        public void HoldItem(GameObject item)
        {
            if (item == null)
                return;

            if (_currentHeldItem != null && _currentGrabbable != null)
            {
                _currentGrabbable.Drop();
            }

            _currentHeldItem = item;
            _currentGrabbable = item.GetComponent<GrabbableInteractable>();

            if (_currentHeldItem != null && (heldItemOffset != Vector3.zero || heldItemRotation != Vector3.zero))
            {
                Transform itemTransform = _currentHeldItem.transform;
                itemTransform.SetLocalPositionAndRotation(heldItemOffset, Quaternion.Euler(heldItemRotation));
            }

            ItemGrabEvent?.Execute();
        }

        public void ReleaseItem()
        {
            _currentHeldItem = null;
            _currentGrabbable = null;
            ItemDropEvent?.Execute();
        }

        public void UseItem()
        {
            if (_currentHeldItem != null && _currentGrabbable != null && _isUsingItem)
            {
                _currentGrabbable.Use();
                ItemUseEvent?.Execute();
            }
        }
    }
}
