using BachelorProject.Events;
using BachelorProject.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BachelorProject.Saving
{
    public class SceneSaver : MonoBehaviour
    {
        private const string LEVEL_SAVE = "LastScene";

        public ObjectIdentifier gameSystems;
        public SceneNameReference nullContinueScene;
        public BoolEvent hasSavedGame;

        private void Start()
        {
            if (PlayerPrefs.HasKey(LEVEL_SAVE))
            {
                hasSavedGame?.Execute(true);
            }
            else
            {
                hasSavedGame?.Execute(false);
            }
        }

        public void ResetSaves()
        {
            PlayerPrefs.DeleteAll();
        }

        public void SaveScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString(LEVEL_SAVE, sceneName);
            Debug.Log($"Postup uložen - {sceneName}");
        }

        public void DebugPrintScene()
        {
            Debug.Log("Uložená scéna: " + GetLatestScene());
        } 

        private string GetLatestScene() => PlayerPrefs.HasKey(LEVEL_SAVE) ? PlayerPrefs.GetString(LEVEL_SAVE) : "NULL";
        
        private string GetCurrentScene() => SceneManager.GetActiveScene().name;

        public void LoadLatestScene()
        {
            var lastScene = ScriptableObject.CreateInstance<SceneNameReference>();
            lastScene.Value = GetLatestScene();
            
            if (!gameSystems.GetSourceComponent(out SceneLoader sc))
            {
                Debug.LogError("Nelze najít scene loader v GameSystems");
                return;
            }
            
            if (Application.CanStreamedLevelBeLoaded(lastScene.Value))
            {
                sc.LoadSceneCall(lastScene);
            }
            else
            {
                Debug.LogError($"Scéna {lastScene.Value} nebyla nalezena v buildu. Načítám hru od začátku.");
                sc.LoadSceneCall(nullContinueScene);
            }
        }
    }
}
