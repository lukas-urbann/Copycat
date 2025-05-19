using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Events
{
    public class ParameterEvent : GameEvent
    {
        private List<ParameterEventListener> _listeners = new();
        
        public override void RegisterListener(GameEventListener listener)
        {
            if (listener is ParameterEventListener parameterListener && !_listeners.Contains(parameterListener))
            {
                _listeners.Add(parameterListener);
            }
        }

        public override void UnregisterListener(GameEventListener listener)
        {
            if (listener is ParameterEventListener parameterListener)
            {
                _listeners.Remove(parameterListener);
            }
        }
        
        protected void Execute(object parameter = null)
        {
            if (parameter == null)
            {
                Debug.Log($"Parameter předaný do {typeof(ParameterEvent)} je null!");
            }
            
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventExecuted(parameter);
            }
        }
    }
}

