using UnityEngine;

namespace BachelorProject.DebugTools
{
    public class DebugPrinter : MonoBehaviour
    {
        public void Print(object message)
        {
            Debug.Log($"Typ: {message?.GetType()} - Message: {message}");
        }
    }
}
