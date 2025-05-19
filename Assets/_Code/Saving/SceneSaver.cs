using UnityEngine;
using UnityEngine.SceneManagement;

namespace BachelorProject.Saving
{
    public class SceneSaver : MonoBehaviour
    {
        private const string LEVEL_SAVE = "LastScene";

        public void SaveScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString(LEVEL_SAVE, sceneName);
            Debug.Log($"Postup uložen - {sceneName}");
        }

        private string GetLatestScene() => PlayerPrefs.HasKey(LEVEL_SAVE) ? PlayerPrefs.GetString(LEVEL_SAVE) : "NULL";

        public void LoadLatestScene()
        {
            StringVariable lastScene = new(PlayerPrefs.GetString(LEVEL_SAVE));
            
            if (Application.CanStreamedLevelBeLoaded(lastScene.Value))
            {
                
            }
            else
            {
                Debug.LogError($"Scéna {lastScene.Value} nebyla nalezena v buildu. Načítám hru od začátku.");
            }
        }
    }
}
