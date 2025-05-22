using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Events
{
    /// <summary>
    /// Jeden z nejdulezitejsich mechanismu hry.
    /// Umoznuje komunikaci mezi objekty bez pevnych linku.
    /// Genericky typ umoznuje extremni flexibilitu pri praci mezi skripty
    /// </summary>
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
        
        /// <summary>
        /// Vyvola event a preda mu parametr, jehoz typ je bezpecne pretypovany v dedici tride
        /// </summary>
        /// <param name="parameter"></param>
        protected void Execute(object parameter = null)
        {
            if (parameter == null)
            {
                Debug.Log($"Parameter predany do {typeof(ParameterEvent<T>)} je null!");
            }
            
            Debug.Log($"{this.name} - Vyvolani eventu s parametrem: {parameter}");
            
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventExecuted(parameter);
            }
        }
    }
}

