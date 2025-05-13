using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Actions
{
    public class BaseScriptActions : MonoBehaviour
    {
        public UnityEvent m_OnAwake;
        public UnityEvent m_OnEnable;
        public UnityEvent m_OnStart;
        public UnityEvent m_OnDisable;
        public UnityEvent m_OnDestroy;

        private void OnAwake() => m_OnAwake?.Invoke();
        private void OnEnable() => m_OnEnable?.Invoke();
        private void Start() => m_OnStart?.Invoke();
        private void OnDisable() => m_OnDisable?.Invoke();
        private void OnDestroy() => m_OnDestroy?.Invoke();
    }
}