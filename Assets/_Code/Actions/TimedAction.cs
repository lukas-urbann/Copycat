using System;
using System.Collections;
using BachelorProject.Events;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Actions
{
    public class TimedAction : MonoBehaviour
    {
        [Header("Timed Action")]
        [SerializeField] private bool startOnEnable = false;
        [SerializeField] private bool actionLoop = false;
        [SerializeField] private FloatReference actionDelay;
        [SerializeField] private VoidEvent onDelayStart;
        [SerializeField] private VoidEvent onDelayComplete;
        public UnityEvent onDelayStartUnityEvent = new UnityEvent();
        public UnityEvent onDelayCompleteUnityEvent = new UnityEvent();

        private void OnEnable()
        {
            if (startOnEnable)
            {
                Execute();
            }
        }

        private void Execute()
        {
            StartCoroutine(DelayCoroutine());
        }
        
        public void ToggleLoop() => actionLoop = !actionLoop;
        public void ToggleLoop(bool val) => actionLoop = val;
        
        private IEnumerator DelayCoroutine()
        {
            onDelayStart?.Execute();
            onDelayStartUnityEvent?.Invoke();
            yield return new WaitForSeconds(actionDelay.Value);
            onDelayComplete?.Execute();
            onDelayCompleteUnityEvent?.Invoke();
            
            //Killswitch pro jistotu
            if (actionDelay.Value < 0.1f)
            {
                actionLoop = false;
            }
            
            if (actionLoop)
            {
                Execute();
            }
        }
    }
}