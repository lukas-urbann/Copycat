using BachelorProject.Events;
using BachelorProject.Management;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    public enum CollisionType
    {
        Trigger,
        Collider
    }

    public class CollisionInteractable : BaseInteractable
    {
        [Header("Collision Interactable")]
        [SerializeField] private VoidEvent OnCollideEnter;
        [SerializeField] private VoidEvent onCollideExit;
        
        public CollisionType collisionType = CollisionType.Trigger;
        public List<ObjectIdentifier> acceptedObjects = new();

        public UnityEvent onCollideEnterUnityEvent = new UnityEvent();
        public UnityEvent onCollideExitUnityEvent = new UnityEvent();

        private void OnCollisionEnter(Collision collision)
        {
            if (collisionType == CollisionType.Trigger) return;
            if (!allowInteractions) return;
            if (!collision.gameObject.CompareTag("Player") && collision.gameObject.layer != LayerMask.NameToLayer("Interactable")) return;

            if (collision.gameObject.TryGetComponent(out IdentifiableObject id))
            {
                if (acceptedObjects.Count > 0 && !acceptedObjects.Contains(id.identifier))
                {
                    return;
                }
            }
            else
            {
                return;
            }
            
            OnCollideEnter?.Execute();
            onCollideEnterUnityEvent?.Invoke();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collisionType == CollisionType.Trigger) return;
            if (!allowInteractions) return;
            if (!collision.gameObject.CompareTag("Player") && collision.gameObject.layer != LayerMask.NameToLayer("Interactable")) return;

            if (collision.gameObject.TryGetComponent(out IdentifiableObject id))
            {
                if (acceptedObjects.Count > 0 && !acceptedObjects.Contains(id.identifier))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            onCollideExit?.Execute();
            onCollideExitUnityEvent?.Invoke();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collisionType == CollisionType.Collider) return;
            if (!allowInteractions) return;
            if (!collision.gameObject.CompareTag("Player") && collision.gameObject.layer != LayerMask.NameToLayer("Interactable")) return;

            if (collision.gameObject.TryGetComponent(out IdentifiableObject id))
            {
                if (acceptedObjects.Count > 0 && !acceptedObjects.Contains(id.identifier))
                {
                    return;
                }
            }
            else
            {
                return;
            }


            OnCollideEnter?.Execute();
            onCollideEnterUnityEvent?.Invoke();
        }
        
        private void OnTriggerExit(Collider collision)
        {
            if (collisionType == CollisionType.Collider) return;
            if (!allowInteractions) return;
            if (!collision.gameObject.CompareTag("Player") && collision.gameObject.layer != LayerMask.NameToLayer("Interactable")) return;

            if (collision.gameObject.TryGetComponent(out IdentifiableObject id))
            {
                if (acceptedObjects.Count > 0 && !acceptedObjects.Contains(id.identifier))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            onCollideExit?.Execute();
            onCollideExitUnityEvent?.Invoke();
        }
        
        public override void Interact()
        {
        }

        public override void Use()
        {
        }
    }
}