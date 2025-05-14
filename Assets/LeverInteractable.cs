using UnityEngine;

namespace BachelorProject.Interactable
{
    public class LeverInteractable : UseableInteractable
    {
        [SerializeField] private bool toggledOn = false;

        public void ToggleLever(bool val)
        {
            toggledOn = val;
            
        }
    }
}