using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    public class UseableInteractable : BaseInteractable
    {
        public GameEvent eventOnInteract;
        public GameEvent eventOnUse;
        
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

