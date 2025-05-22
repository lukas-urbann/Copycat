using UnityEngine;

namespace BachelorProject.Events
{
    /// <summary>
    /// Základ pro herní eventy
    /// </summary>
    public abstract class GameEvent : ScriptableObject
    {
        public abstract void RegisterListener(GameEventListener listener);
        public abstract void UnregisterListener(GameEventListener listener);
    }
}