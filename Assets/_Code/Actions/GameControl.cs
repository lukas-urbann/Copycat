using UnityEngine;

namespace BachelorProject.Actions
{
    public class GameControl : MonoBehaviour
    {
        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            
            Debug.Log("Ukončuji hru");
            Application.Quit();
        }
    }
}
