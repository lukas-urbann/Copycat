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
            if (ObjectRegistry.GetObject(loadingSceneIdentifier, out var load))
            {
                load.SetActive(true);
            }
            else
            {
                Debug.LogError("Loading screen není v registry");
            }
            
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (asyncLoad is { isDone: false })
            {
                yield return null;
            }
            
            load.SetActive(false);
        }
    }
}
