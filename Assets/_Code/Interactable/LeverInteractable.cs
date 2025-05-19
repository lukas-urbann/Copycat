using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    public class LeverInteractable : UseableInteractable
    {
        [Header("Lever Interactable")]
        [SerializeField] private bool toggledOn = false;
        [SerializeField] private Animator leverAnimator;

        private void OnEnable()
        {
            UnityEventOnInteract = new UnityEvent();
            UnityEventOnInteract.AddListener(ToggleLever);
            UnityEventOnUse = new UnityEvent();
            UnityEventOnUse.AddListener(ToggleLeverState);
        }

        private void Start()
        {
            if (leverAnimator == null)
            {
                if (!TryGetComponent(out leverAnimator))
                {
                    Debug.LogError("P�ka nem� animator");
                }
            }

            leverAnimator.SetBool("toggledOn", toggledOn);
        }

        public void ToggleLever()
        {
            Debug.Log("Toggling lever");
            Use();
        }

        public void ToggleLeverState()
        {
            leverAnimator.SetBool("toggledOn", toggledOn = !toggledOn);
        }
    }
}