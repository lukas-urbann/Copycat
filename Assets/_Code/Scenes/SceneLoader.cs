using System.Collections;
using BachelorProject.Management;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BachelorProject.Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        public ObjectIdentifier loadingSceneIdentifier;

        public void LoadSceneCall(StringVariable sceneId)
        {
            StartCoroutine(LoadScene(sceneId.Value));
        }
        
        private IEnumerator LoadScene(string sceneName)
        {
            loadingSceneIdentifier.GetSourceComponent<CanvasGroup>().alpha = 1;
            
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (asyncLoad is { isDone: false })
            {
                yield return null;
            }
            
            loadingSceneIdentifier.GetSourceComponent<CanvasGroup>().alpha = 0;
        }
    }
}
