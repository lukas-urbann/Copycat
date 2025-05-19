using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Events
{
    public class VoidEventListener : GameEventListener
    {
        public UnityEvent response;

        public override void OnEventExecuted()
        {
            response.Invoke();
        }
        
        public override void OnEventExecuted(object parameter)
        {
            Debug.LogError($"{typeof(VoidEventListener)} neumí přijímat argumenty!");
        }
    }
}