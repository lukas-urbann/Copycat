using UnityEngine;

namespace BachelorProject.DebugTools
{
    /// <summary>
    /// Jen pro print při debugu.
    /// </summary>
    public class DebugPrinter : MonoBehaviour
    {
        public void Print(object message)
        {
            Debug.Log($"Typ: {message?.GetType()} - Message: {message}");
        }
    }
}
