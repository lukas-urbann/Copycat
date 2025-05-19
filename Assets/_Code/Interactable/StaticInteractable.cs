using BachelorProject.Events;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    public class StaticInteractable : BaseInteractable
    {
        [Header("Static Interactable")]
        public VoidEvent onInteract;
        public UnityEvent onInteractEvent;

        public override void Interact()
        {
            onInteract?.Execute();
            onInteractEvent?.Invoke();
        }

        public override void Use()
        {
            Debug.Log($"Nelze použít {transform.root.name}");
        }
    }
}