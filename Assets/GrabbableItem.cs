using BachelorProject.Player;
using System.Collections;
using UnityEngine;
using BachelorProject.Helper;

namespace BachelorProject.Interactable
{
    public class GrabbableItem : BaseItem
    {
        [SerializeField] private FloatReference lerpSpeed;
        [SerializeField] private FloatReference snapDistance;
        public ObjectIdentifier playerRoot;

        private bool isGrabbed = false;
        private bool isBeingLerped = false;
        private UnityEngine.Rendering.ShadowCastingMode originalShadowMode;

        public Transform handTransform;
        public Rigidbody itemRigidbody;
        public Collider itemCollider;

        private void Awake()
        {
            TryAssignComponents();
        }

        private void TryAssignComponents()
        {
            itemRigidbody = itemRigidbody != null ? itemRigidbody : Components.GetComponentInChildrenRecursively<Rigidbody>(transform);
            itemCollider = itemCollider != null ? itemCollider : Components.FindComponentFromRoot<Collider>(transform);
            meshRenderer = meshRenderer != null ? meshRenderer : Components.FindComponentFromRoot<MeshRenderer>(transform);
        }

        public override void TryConsume()
        {
            Debug.Log($"{transform.root.name} - Consume");
        }

        public override void Interact()
        {
            if (IdentifiableObjectRegistry.GetObject(playerRoot) == null)
            {
                Debug.LogWarning($"{transform.root.name}: Root hráèe není v registry, nemùžu grabnout.");
                return;
            }

            if (!isGrabbed && !isBeingLerped)
            {
                if (itemRigidbody != null)
                {
                    itemRigidbody.isKinematic = true;
                    itemRigidbody.useGravity = false;
                }

                isBeingLerped = true;
                StartCoroutine(LerpToHand());
            }
        }

        private IEnumerator LerpToHand()
        {
            //Le k ruce, dokud jsi daleko
            while (Vector3.Distance(transform.position, handTransform.position) > snapDistance.Value)
            {
                transform.SetPositionAndRotation(
                    Vector3.Lerp(transform.position, handTransform.position, Time.deltaTime * lerpSpeed.Value),
                    Quaternion.Slerp(transform.rotation, handTransform.rotation, Time.deltaTime * lerpSpeed.Value));

                yield return null;
            }

            //Snapni se k ruce
            transform.SetPositionAndRotation(handTransform.position, handTransform.rotation);
            transform.SetParent(handTransform);

            isBeingLerped = false;
            isGrabbed = true;

            if (itemCollider != null)
            {
                itemCollider.enabled = false;
            }

            if (meshRenderer != null)
            {
                originalShadowMode = meshRenderer.shadowCastingMode;
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }

            IdentifiableObjectRegistry.GetObject(playerRoot).GetComponent<PlayableCharacterHand>().HoldItem(gameObject);
        }

        public void Drop()
        {
            if (!isGrabbed) return;

            transform.SetParent(null);

            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = false;
                itemRigidbody.useGravity = true;
                itemRigidbody.AddForce(handTransform.forward * 2f, ForceMode.Impulse);
            }

            if (itemCollider != null)
            {
                itemCollider.enabled = true;
            }

            if (meshRenderer != null)
            {
                meshRenderer.shadowCastingMode = originalShadowMode;
            }

            IdentifiableObjectRegistry.GetObject(playerRoot).GetComponent<PlayableCharacterHand>().ReleaseItem();
            isGrabbed = false;

            OnUnsee();
        }
    }
}
