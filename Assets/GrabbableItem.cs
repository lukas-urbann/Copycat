using BachelorProject.Player;
using System.Collections;
using UnityEngine;
using BachelorProject.Helper;

namespace BachelorProject.Interactable
{
    public class GrabbableItem : BaseItem
    {
        [SerializeField] private float lerpSpeed = 15f;
        [SerializeField] private float snapDistance = 0.05f;

        public ObjectIdentifier playerRoot;

        // Private variables
        private bool isGrabbed = false;
        private bool isBeingLerped = false;
        public Transform handTransform;
        public Rigidbody itemRigidbody;
        public Collider itemCollider;

        private void Awake()
        {
            itemRigidbody = Components.GetComponentInChildrenRecursively<Rigidbody>(transform);
            itemCollider = Components.GetComponentInChildrenRecursively<Collider>(transform);
            meshRenderer = Components.GetComponentInChildrenRecursively<MeshRenderer>(transform);
        }

        public override void Interact()
        {
            if (!isGrabbed && !isBeingLerped)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    if (handTransform != null)
                    {
                        if (IdentifiableObjectRegistry.GetObject(playerRoot) != null)
                        {
                            StartGrabProcess();
                        }
                        else
                        {
                            Debug.LogWarning("HandController component not found on Hand transform!");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Hand transform not found on player!");
                    }
                }
            }
            else if (isGrabbed)
            {
                // If already grabbed, we could implement a drop mechanic here
                Drop();
            }
        }

        private void StartGrabProcess()
        {
            // Disable physics
            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = true;
                itemRigidbody.useGravity = false;
            }

            // Start lerping coroutine
            isBeingLerped = true;
            StartCoroutine(LerpToHand());
        }

        private IEnumerator LerpToHand()
        {
            // Continue until we're close enough to the hand
            while (Vector3.Distance(transform.position, handTransform.position) > snapDistance)
            {
                // Lerp position and rotation
                transform.position = Vector3.Lerp(transform.position, handTransform.position, Time.deltaTime * lerpSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, handTransform.rotation, Time.deltaTime * lerpSpeed);

                yield return null;
            }

            // Once close enough, snap to hand and attach
            transform.position = handTransform.position;
            transform.rotation = handTransform.rotation;
            transform.SetParent(handTransform);

            // Update states
            isBeingLerped = false;
            isGrabbed = true;

            // Disable collider while grabbed
            if (itemCollider != null)
            {
                itemCollider.enabled = false;
            }

            // Notify hand controller
            IdentifiableObjectRegistry.GetObject(playerRoot).GetComponent<PlayableCharacterHand>().HoldItem(gameObject);
        }

        public void Drop()
        {
            if (isGrabbed)
            {
                // Detach from hand
                transform.SetParent(null);

                // Re-enable physics
                if (itemRigidbody != null)
                {
                    itemRigidbody.isKinematic = false;
                    itemRigidbody.useGravity = true;

                    // Optional: Add a small force to "throw" the item
                    itemRigidbody.AddForce(handTransform.forward * 2f, ForceMode.Impulse);
                }

                // Re-enable collider
                if (itemCollider != null)
                {
                    itemCollider.enabled = true;
                }

                // Notify hand controller
                IdentifiableObjectRegistry.GetObject(playerRoot).GetComponent<PlayableCharacterHand>().ReleaseItem();

                // Reset states
                isGrabbed = false;

                // Restore original material
                if (meshRenderer != null && originalMaterial != null)
                {
                    meshRenderer.material = originalMaterial;
                }
            }
        }

        public override void TryConsume()
        {
            Debug.Log("Consume");
        }
    }
}
