using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Events
{
    public class ParameterEvent<T> : GameEvent
    {
        private List<ParameterEventListener<T>> _listeners = new();
        
        public override void RegisterListener(GameEventListener listener)
        {
            if (listener is ParameterEventListener<T> parameterListener && !_listeners.Contains(parameterListener))
            {
                _listeners.Add(parameterListener);
            }
        }

        public override void UnregisterListener(GameEventListener listener)
        {
            if (listener is ParameterEventListener<T> parameterListener)
            {
                _listeners.Remove(parameterListener);
            }
        }
        
        protected void Execute(object parameter = null)
        {
            if (parameter == null)
            {
                Debug.Log($"Parameter předaný do {typeof(ParameterEvent<T>)} je null!");
            }
            
            Debug.Log($"Vyvolání eventu typu {typeof(ParameterEvent<T>)} s parametrem: {parameter}");
            
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventExecuted(parameter);
            }
        }
    }
}

