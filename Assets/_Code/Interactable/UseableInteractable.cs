using BachelorProject.Events;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    /// <summary>
    /// Zaklad pro pouzitelne interaktovatelne objekty.
    /// Spousti eventy. UnityEventy lze volat z dedicich skriptu a void eventy pro interakci s ostatnimi objekty na scene.
    /// Dedi z nej vetsina interaktovatelnych objektu na scene.
    /// </summary>
    public class UseableInteractable : BaseInteractable
    {
        [Header("Useable Interactable")]
        public VoidEvent eventOnInteract;
        public VoidEvent eventOnUse;
        
        protected UnityEvent UnityEventOnInteract;
        protected UnityEvent UnityEventOnUse;
        
        public void DestroyScript()
        {
            Destroy(this);
        }

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

