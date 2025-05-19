using BachelorProject.Player;
using System.Collections;
using UnityEngine;
using BachelorProject.Helper;
using BachelorProject.Management;

namespace BachelorProject.Interactable
{
    public class GrabbableInteractable : BaseInteractable
    {
        [Header("Grabbable Interactable")]
        [SerializeField] private FloatReference lerpSpeed;
        [SerializeField] private FloatReference snapDistance;
        [SerializeField] private ObjectIdentifier playerHandIdentifier;
        public ObjectIdentifier playerRoot;

        private bool isGrabbed = false;
        private bool isBeingLerped = false;
        private UnityEngine.Rendering.ShadowCastingMode originalShadowMode;
        private Rigidbody itemRigidbody;
        private Collider itemCollider;

        private void Awake()
        {
            TryAssignComponents();
        }

        private void TryAssignComponents()
        {
            itemRigidbody = itemRigidbody != null ? itemRigidbody : Components.GetComponentInChildrenRecursively<Rigidbody>(transform);
            itemCollider = itemCollider != null ? itemCollider : Components.GetComponentInChildrenRecursively<Collider>(transform);
            meshRenderer = meshRenderer != null ? meshRenderer : Components.GetComponentInChildrenRecursively<MeshRenderer>(transform);
        }

        public override void Use()
        {
            Debug.Log($"{gameObject.name} - Use");
        }

        public override void Interact()
        {
            if (ObjectRegistry.GetObject(playerRoot) == null)
            {
                Debug.LogWarning($"{transform.root.name}: Root hr��e nen� v registry, nem��u grabnout.");
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
            if (!ObjectRegistry.GetObject(playerHandIdentifier, out var go)) yield break;

            //Le� k ruce, dokud jsi daleko
            while (Vector3.Distance(transform.position, go.transform.position) > snapDistance.Value)
            {
                transform.SetPositionAndRotation(
                    Vector3.Lerp(transform.position, go.transform.position, Time.deltaTime * lerpSpeed.Value),
                    Quaternion.Slerp(transform.rotation, go.transform.rotation, Time.deltaTime * lerpSpeed.Value));

                yield return null;
            }

            //Snapni se k ruce
            transform.SetPositionAndRotation(go.transform.position, go.transform.rotation);
            transform.SetParent(go.transform);

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

            ObjectRegistry.GetObject(playerRoot).GetComponent<PlayableCharacterHand>().HoldItem(gameObject);
        }

        public void Drop()
        {
            if (!isGrabbed) return;

            transform.SetParent(null);

            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = false;
                itemRigidbody.useGravity = true;

                if (ObjectRegistry.GetObject(playerHandIdentifier, out var go))
                {
                    itemRigidbody.AddForce(go.transform.forward * 2f, ForceMode.Impulse);
                }
            }

            if (itemCollider != null)
            {
                itemCollider.enabled = true;
            }

            if (meshRenderer != null)
            {
                meshRenderer.shadowCastingMode = originalShadowMode;
            }

            ObjectRegistry.GetObject(playerRoot).GetComponent<PlayableCharacterHand>().ReleaseItem();
            isGrabbed = false;

            //OnUnsee();
        }
    }
}
