using BachelorProject.Interactable;
using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Player
{
    public class PlayableCharacterHand : MonoBehaviour
    {
        [Header("Holding Settings")]
        [Tooltip("Optional offset to apply to held items")]
        [SerializeField] private Vector3 heldItemOffset = Vector3.zero;

        [Tooltip("Optional rotation to apply to held items")]
        [SerializeField] private Vector3 heldItemRotation = Vector3.zero;

        [Tooltip("Maximum number of items that can be held at once (0 = unlimited)")]
        [SerializeField] private int maxHeldItems = 1;

        // Private variables
        private List<GameObject> heldItems = new List<GameObject>();
        private GameObject currentHeldItem;

        /// <summary>
        /// Called by GrabbableItem to notify the hand that it's now holding an item
        /// </summary>
        /// <param name="item">The item being held</param>
        public void HoldItem(GameObject item)
        {
            // Check if we can hold more items
            if (maxHeldItems > 0 && heldItems.Count >= maxHeldItems)
            {
                // If we're at capacity, drop the first item to make room
                if (heldItems.Count > 0)
                {
                    GameObject oldItem = heldItems[0];
                    GrabbableItem grabbable = oldItem.GetComponent<GrabbableItem>();
                    if (grabbable != null)
                    {
                        grabbable.Drop();
                    }
                }
            }

            // Add to our held items list
            if (!heldItems.Contains(item))
            {
                heldItems.Add(item);
                currentHeldItem = item;

                // Apply custom positioning if needed
                if (heldItemOffset != Vector3.zero || heldItemRotation != Vector3.zero)
                {
                    item.transform.localPosition = heldItemOffset;
                    item.transform.localRotation = Quaternion.Euler(heldItemRotation);
                }

                Debug.Log("Now holding: " + item.name);

                // You can call any additional methods here, like updating UI
                OnItemPickedUp(item);
            }
        }

        /// <summary>
        /// Called by GrabbableItem when an item is dropped
        /// </summary>
        public void ReleaseItem(GameObject item = null)
        {
            if (item == null)
            {
                // If no specific item is provided, release the current item
                item = currentHeldItem;
            }

            if (item != null && heldItems.Contains(item))
            {
                heldItems.Remove(item);

                // Update current held item
                currentHeldItem = heldItems.Count > 0 ? heldItems[heldItems.Count - 1] : null;

                Debug.Log("Released: " + item.name);

                // You can call any additional methods here, like updating UI
                OnItemDropped(item);
            }
        }

        public void ConsumeItem()
        {
            if (currentHeldItem != null)
            {
                // Call the Consume method on the item
                GrabbableItem grabbable = currentHeldItem.GetComponent<GrabbableItem>();
                if (grabbable != null)
                {
                    grabbable.TryConsume();
                }
                // Release the item after consuming
                ReleaseItem(currentHeldItem);
            }
        }

        /// <summary>
        /// Called when an item is picked up
        /// </summary>
        /// <param name="item">The item that was picked up</param>
        protected virtual void OnItemPickedUp(GameObject item)
        {
            // This is a virtual method that can be overridden in derived classes
            // for custom behavior when an item is picked up

            // For example, you might want to animate the hand, play a sound, etc.
        }

        /// <summary>
        /// Called when an item is dropped
        /// </summary>
        /// <param name="item">The item that was dropped</param>
        protected virtual void OnItemDropped(GameObject item)
        {
            // This is a virtual method that can be overridden in derived classes
            // for custom behavior when an item is dropped

            // For example, you might want to animate the hand, play a sound, etc.
        }

        /// <summary>
        /// Check if the hand is currently holding any items
        /// </summary>
        public bool IsHoldingItem()
        {
            return heldItems.Count > 0;
        }

        /// <summary>
        /// Get the currently held item
        /// </summary>
        public GameObject GetCurrentHeldItem()
        {
            return currentHeldItem;
        }

        /// <summary>
        /// Get a list of all currently held items
        /// </summary>
        public List<GameObject> GetAllHeldItems()
        {
            return new List<GameObject>(heldItems);
        }
    }
}