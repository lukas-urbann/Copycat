using UnityEngine;

namespace BachelorProject.Scenes
{
    /// <summary>
    /// Slouzi k zavolani nacteni sceny z jineho objektu
    /// </summary>
    public class SceneLoadCall : MonoBehaviour
    {
        public ObjectIdentifier gameSystems;

        public void LoadScene(SceneNameReference sceneName)
        {
            if (!sceneName)
            {
                Debug.LogError("SceneLoadCall: Scene je null");
                return;
            }
            
            if (gameSystems.GetSourceComponent(out SceneLoader sc))
            {
                sc.LoadSceneCall(sceneName);
            }
        }
    }
}