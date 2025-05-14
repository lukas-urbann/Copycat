using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    public class CollectibleInteractable : UseableInteractable
    {
        private void OnEnable()
        {
            UnityEventOnInteract = new UnityEvent();
            UnityEventOnInteract.AddListener(Collect);
        }

        private void Collect()
        {
            
        }
    }
}