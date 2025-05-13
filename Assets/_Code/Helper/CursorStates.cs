using UnityEngine;

namespace BachelorProject.Helper
{
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