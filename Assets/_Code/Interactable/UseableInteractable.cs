using BachelorProject.Events;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    public class UseableInteractable : BaseInteractable
    {
        [Header("Useable Interactable")]
        public VoidEvent eventOnInteract;
        public VoidEvent eventOnUse;
        
        protected UnityEvent UnityEventOnInteract;
        protected UnityEvent UnityEventOnUse;
        
        public override void Interact()
        {
            eventOnInteract?.Execute();
            UnityEventOnInteract?.Invoke();
        }

        public override void Use()
        {
            eventOnUse?.Execute();
            UnityEventOnUse?.Invoke();
        }
    }
}

