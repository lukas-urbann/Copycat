using BachelorProject.Events;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    /// <summary>
    /// Staticky interactable.
    /// Primarne urcen pro interakci s objekty, ktere nemaji zadnou fyziku.
    /// Pouziva se napriklad u papiru
    /// </summary>
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
            Debug.Log($"Nelze pouzit {transform.root.name}");
        }
    }
}