using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Events
{
    [CreateAssetMenu(menuName = "Events/Void Event")]
    public class VoidEvent : GameEvent
    {
        private List<GameEventListener> _listeners = new();

        public void Execute()
        {
            Debug.Log($"{GetType().Name} - Vyvolání void eventu");
            
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventExecuted();
            }
        }

        public override void RegisterListener(GameEventListener listener)
        {
            if (listener is VoidEventListener voidListener && !_listeners.Contains(voidListener))
            {
                _listeners.Add(voidListener);
            }
        }

        public override void UnregisterListener(GameEventListener listener)
        {
            if (listener is VoidEventListener voidListener)
            {
                _listeners.Remove(voidListener);
            }
        }
    }
}