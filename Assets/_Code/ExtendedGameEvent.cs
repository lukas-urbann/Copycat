using System;
using BachelorProject.Events;
using UnityEngine.Events;

[Serializable]
public class ExtendedGameEvent
{
    public int asd;
    public GameEvent gameEvent;
    public UnityEvent unityEvent = new();
    
    public void Execute()
    {
        gameEvent?.Execute();
        unityEvent?.Invoke();
    }
}
