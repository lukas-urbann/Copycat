using UnityEngine;

namespace BachelorProject.Helper
{
    /// <summary>
    /// Helper pro práci s kurzorem při přechodu z menu do hry
    /// </summary>
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(0)]
    public class CursorStates : MonoBehaviour
    {
        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}