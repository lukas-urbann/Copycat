using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Events
{
    /// <summary>
    /// Void event je specialni tim, ze nenese parametr.
    /// Tudiz se da vyuzit naprosto kdekoliv k pouhemu oznameni udalosti
    /// </summary>
    public class VoidEventListener : GameEventListener
    {
        public UnityEvent response;

        public override void OnEventExecuted()
        {
            response.Invoke();
        }
        
        /// <summary>
        /// Pro pripad ze se o to nekdo pokusi
        /// </summary>
        public override void OnEventExecuted(object parameter)
        {
            Debug.LogError($"{typeof(VoidEventListener)} neumi prijimat argumenty!");
        }
    }
}