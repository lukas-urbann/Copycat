using UnityEngine;

namespace BachelorProject.Interactable
{
    public class StaticInteractable : BaseInteractable
    {
        public override void Interact()
        {
            Debug.Log($"Nelze interagovat s {transform.root.name}");
        }

        public override void Use()
        {
            Debug.Log($"Nelze použít {transform.root.name}");
        }
    }
}