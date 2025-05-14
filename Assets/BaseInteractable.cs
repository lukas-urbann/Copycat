using BachelorProject.UI;
using UnityEngine;

namespace BachelorProject.Interactable
{
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {
        private Material _originalMaterial;
        public MeshRenderer meshRenderer;
        [SerializeField] protected bool allowInteractions = true;
        [SerializeField] protected StringVariable interactionVariable;
        [SerializeField] protected Material highlightMaterial;
        [SerializeField] protected ObjectIdentifier interactionLabel;

        public bool IsInteractable { get => allowInteractions; }

        private void Start()
        {
            if (meshRenderer != null && highlightMaterial != null)
            {
                _originalMaterial = meshRenderer.material;
            }
        }

        public void ToggleInteractability(bool val)
        {
            allowInteractions = val;

            if (meshRenderer != null && highlightMaterial != null)
            {
                meshRenderer.material = val ? meshRenderer.material : _originalMaterial;
            }
        }

        public abstract void Interact();

        //Alternativní metoda pro použití předmětu.
        public abstract void Use();

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

            if (meshRenderer != null && _originalMaterial != null)
            {
                meshRenderer.material = _originalMaterial;
            }
        }
    }
}