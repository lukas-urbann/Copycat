using BachelorProject.Audio;
using BachelorProject.Events;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    /// <summary>
    /// Zakladni skript pro packy.
    /// Packy mohou byt ve stavu True/False. Pro lepsi dostupnost
    /// vyuzivaji BoolEventu.
    /// </summary>
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
                    Debug.LogError("Paka nema animator");
                }
            }

            if (leverAnimator)
                leverAnimator.SetBool("toggledOn", toggledOn);
            
            if (leverValue.Variable != null)
                leverValue.Variable.Value = toggledOn;
        }

        /// <summary>
        /// Pri interakci s packou se zavola metoda ToggleLever.
        /// </summary>
        private void ToggleLever()
        {
            Use();
            
            if (leverToggledEvent)
                leverToggledEvent?.Execute();
        }

        /// <summary>
        /// Zmeni vizualni stav packy a aktualizuje stav
        /// </summary>
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