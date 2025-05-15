using BachelorProject.Events;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    public class CollisionInteractable : BaseInteractable
    {
        [SerializeField] private GameEvent onCollisionEnter;
        [SerializeField] private GameEvent onCollisionExit;
        public UnityEvent onCollisionEnterUnityEvent = new UnityEvent();
        public UnityEvent onCollisionExitUnityEvent = new UnityEvent();
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!allowInteractions) return;
            
            if (!collision.gameObject.CompareTag("Player") &&
                collision.gameObject.layer != LayerMask.NameToLayer("Interactable")) return;
            
            onCollisionEnter?.Execute();
            onCollisionEnterUnityEvent?.Invoke();
        }
        
        private void OnCollisionExit(Collision collision)
        {
            if (!allowInteractions) return;
            
            if (!collision.gameObject.CompareTag("Player") &&
                collision.gameObject.layer != LayerMask.NameToLayer("Interactable")) return;
            
            onCollisionExit?.Execute();
            onCollisionExitUnityEvent?.Invoke();
        }
        
        public override void Interact()
        {
        }

        public override void Use()
        {
        }
    }
}