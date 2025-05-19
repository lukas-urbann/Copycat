using UnityEngine;

namespace BachelorProject.Events
{
    public abstract class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;

        private void OnEnable()
        {
            if (gameEvent) gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent) gameEvent.UnregisterListener(this);
        }

        public virtual void OnEventExecuted()
        {
            //Response.Invoke();
            Debug.Log("EventExecuted");
        }

        public virtual void OnEventExecuted(object parameter)
        {
            //Response.Invoke(parameter);
            Debug.Log("EventExecuted s parametrem: " + parameter);
        }
    }
}