using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Events
{
    public abstract class ParameterEventListener<T> : GameEventListener
    {
        public UnityEvent<T> response;

        public override void OnEventExecuted()
        {
            Debug.LogError($"{GetType()} vyžaduje argumenty!");
        }
        
        public override void OnEventExecuted(object parameter)
        {
            if (parameter is T typed)
            {
                response?.Invoke(typed);
            }
            else
            {
                Debug.LogError($"{GetType()} obdržel parametr typu {parameter?.GetType()}, ale očekával {typeof(T)}");
            }
        }
    }
}