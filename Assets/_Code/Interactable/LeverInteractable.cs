using BachelorProject.Audio;
using BachelorProject.Events;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    public class LeverInteractable : UseableInteractable
    {
        [Header("Lever Interactable")] [SerializeField]
        private BoolEvent leverToggledEvent;
        [SerializeField] private BoolReference leverValue;
        [SerializeField] private bool toggledOn = false;
        [SerializeField] private Animator leverAnimator;
        [SerializeField] private AudioCall switchAudio;

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
                    Debug.LogError("Páka nemá animator");
                }
            }

            if (leverAnimator)
                leverAnimator.SetBool("toggledOn", toggledOn);
            
            if (leverValue.Variable != null)
                leverValue.Variable.Value = toggledOn;
        }

        private void ToggleLever()
        {
            Use();
            
            if (leverToggledEvent)
                leverToggledEvent?.Execute();
        }

        public void ToggleLeverState()
        {
            if (switchAudio != null)
                switchAudio.Play();
            
            if (leverAnimator != null)
                leverAnimator.SetBool("toggledOn", toggledOn = !toggledOn);
            
            if (leverValue.Variable != null)
                leverValue.Variable.Value = toggledOn;
        }
    }
}