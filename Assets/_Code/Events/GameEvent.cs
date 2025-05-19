using UnityEngine;

namespace BachelorProject.Events
{
    public abstract class GameEvent : ScriptableObject
    {
        public abstract void RegisterListener(GameEventListener listener);
        public abstract void UnregisterListener(GameEventListener listener);
    }
}