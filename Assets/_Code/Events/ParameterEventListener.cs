using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Events
{
    public class ParameterEventListener : GameEventListener
    {
        public UnityEvent<object> response;

        public override void OnEventExecuted()
        {
            Debug.LogError($"{typeof(VoidEventListener)} vyžaduje argumenty!");
        }
        
        public override void OnEventExecuted(object parameter)
        {
            response?.Invoke(parameter);
        }
    }
}