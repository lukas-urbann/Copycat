using System.Collections;
using BachelorProject.Management;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BachelorProject.Actions
{
    public class SceneLoader : MonoBehaviour
    {
        public ObjectIdentifier loadingSceneIdentifier;

        public void LoadSceneCall(StringVariable sceneId)
        {
            StartCoroutine(LoadScene(sceneId));
        }
        
        public IEnumerator LoadScene(StringVariable sceneId)
        {
            if (ObjectRegistry.GetObject(loadingSceneIdentifier, out var load))
            {
                load.SetActive(true);
            }
            else
            {
                Debug.LogError("Loading screen není v registry");
            }
            
            var asyncLoad = SceneManager.LoadSceneAsync(sceneId.Value);

            while (asyncLoad is { isDone: false })
            {
                yield return null;
            }
            
            load.SetActive(false);
        }
    }
}
