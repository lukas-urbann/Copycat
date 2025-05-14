using BachelorProject.UI;
using UnityEngine;

namespace BachelorProject.Interactable
{
    public abstract class BaseItem : MonoBehaviour, IInteractable
    {
        protected Material originalMaterial;
        public MeshRenderer meshRenderer;
        [SerializeField] protected bool allowInteractions = true;
        [SerializeField] protected StringVariable interactionVariable;
        [SerializeField] protected Material highlightMaterial;
        [SerializeField] protected ObjectIdentifier interactionLabel;

        private void Start()
        {
            if (meshRenderer != null && highlightMaterial != null)
            {
                originalMaterial = meshRenderer.material;
            }
        }

        public abstract void Interact();

        public abstract void TryConsume();

        public virtual void OnSee()
        {
            GameObject interaction = IdentifiableObjectRegistry.GetObject(interactionLabel);
            if (interaction != null && interaction.TryGetComponent(out InteractionText label))
            {
                label.SetInteractionText(interactionVariable);
            }

            if (meshRenderer != null && highlightMaterial != null && allowInteractions)
            {
                meshRenderer.material = highlightMaterial;
            }
        }

        public virtual void OnUnsee()
        {
            GameObject interaction = IdentifiableObjectRegistry.GetObject(interactionLabel);
            if (interaction != null && interaction.TryGetComponent(out InteractionText label))
            {
                label.ClearInteractionText();
            }

            if (meshRenderer != null && originalMaterial != null)
            {
                meshRenderer.material = originalMaterial;
            }
        }
    }
}