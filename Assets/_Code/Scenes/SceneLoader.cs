using System.Collections;
using BachelorProject.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BachelorProject.Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        public ObjectIdentifier loadingSceneIdentifier;
        public SceneNameReference mainMenuScene;
        public BoolEvent menuSceneLoaded;
        public BoolEvent gameSceneLoaded;

        private void OnEnable()
        {
            SceneCheck();
        }

        private void SceneCheck()
        {
            menuSceneLoaded?.Execute(SceneManager.GetActiveScene().name == mainMenuScene.Value);
            gameSceneLoaded?.Execute(SceneManager.GetActiveScene().name != mainMenuScene.Value);
        }

        public void LoadSceneCall(SceneNameReference sceneId)
        {
            StartCoroutine(LoadScene(sceneId.Value));
        }
        
        private IEnumerator LoadScene(string sceneName)
        {
            loadingSceneIdentifier.GetSourceComponent<CanvasGroup>().alpha = 1;
            
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            
            SceneCheck();
            loadingSceneIdentifier.GetSourceComponent<CanvasGroup>().alpha = 0;
        }
    }
}
