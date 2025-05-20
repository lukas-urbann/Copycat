using BachelorProject.Player;
using System.Collections;
using UnityEngine;
using BachelorProject.Helper;
using BachelorProject.Management;
using BachelorProject.Audio;

namespace BachelorProject.Interactable
{
    public class GrabbableInteractable : BaseInteractable
    {
        [Header("Grabbable Interactable")]
        [SerializeField] private FloatReference lerpSpeed;
        [SerializeField] private FloatReference snapDistance;
        [SerializeField] private ObjectIdentifier playerHandIdentifier;
        public ObjectIdentifier playerRoot;

        [Header("Audio")]
        public AudioCall useAudio;
        public AudioCall interactAudio;
        public AudioCall dropAudio;

        private bool _gamePaused = false;
        private bool _isGrabbed = false;
        private bool _isBeingLerped = false;
        private UnityEngine.Rendering.ShadowCastingMode _originalShadowMode;
        private Rigidbody _itemRigidbody;
        private Collider _itemCollider;

        private void Awake()
        {
            TryAssignComponents();
        }

        private void TryAssignComponents()
        {
            _itemRigidbody = _itemRigidbody != null ? _itemRigidbody : Components.GetComponentInChildrenRecursively<Rigidbody>(transform);
            _itemCollider = _itemCollider != null ? _itemCollider : Components.GetComponentInChildrenRecursively<Collider>(transform);
            meshRenderer = meshRenderer != null ? meshRenderer : Components.GetComponentInChildrenRecursively<MeshRenderer>(transform);
        }
        
        public void ToggleGamePause(bool isPaused)
        {
            _gamePaused = isPaused;
        }

        public override void Use()
        {
            if (_gamePaused) return;
            Debug.Log($"{gameObject.name} - Use");
            useAudio.Play();
        }

        public override void Interact()
        {
            if (_gamePaused) return;
            if (ObjectRegistry.GetObject(playerRoot) == null)
            {
                Debug.LogWarning($"{transform.root.name}: Root hráče není v registry, nemůžu grabnout.");
                return;
            }

            if (!_isGrabbed && !_isBeingLerped)
            {
                if (_itemRigidbody != null)
                {
                    _itemRigidbody.isKinematic = true;
                    _itemRigidbody.useGravity = false;
                }

                _isBeingLerped = true;
                interactAudio.Play();
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

            _isBeingLerped = false;
            _isGrabbed = true;

            if (_itemCollider != null)
            {
                _itemCollider.enabled = false;
            }

            if (meshRenderer != null)
            {
                _originalShadowMode = meshRenderer.shadowCastingMode;
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }

            ObjectRegistry.GetObject(playerRoot).GetComponent<PlayableCharacterHand>().HoldItem(gameObject);
        }

        public void Drop()
        {
            if (_gamePaused) return;
            if (!_isGrabbed) return;

            dropAudio.Play();

            transform.SetParent(null);

            if (_itemRigidbody != null)
            {
                _itemRigidbody.isKinematic = false;
                _itemRigidbody.useGravity = true;

                if (ObjectRegistry.GetObject(playerHandIdentifier, out var go))
                {
                    _itemRigidbody.AddForce(go.transform.forward * 2f, ForceMode.Impulse);
                }
            }

            if (_itemCollider != null)
            {
                _itemCollider.enabled = true;
            }

            if (meshRenderer != null)
            {
                meshRenderer.shadowCastingMode = _originalShadowMode;
            }

            ObjectRegistry.GetObject(playerRoot).GetComponent<PlayableCharacterHand>().ReleaseItem();
            _isGrabbed = false;

            //OnUnsee();
        }
    }
}
