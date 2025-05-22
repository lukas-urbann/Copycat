using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Events
{
    /// <summary>
    /// Listner pro generic eventy
    /// </summary>
    public abstract class ParameterEventListener<T> : GameEventListener
    {
        public UnityEvent<T> response;

        public override void OnEventExecuted()
        {
            Debug.LogError($"{GetType()} vyzaduje argumenty!");
        }
        
        public override void OnEventExecuted(object parameter)
        {
            if (parameter is T typed)
            {
                response?.Invoke(typed);
            }
            else
            {
                Debug.LogError($"{GetType()} obdrzel parametr typu {parameter?.GetType()}, ale ocekaval {typeof(T)}");
            }
        }
    }
}