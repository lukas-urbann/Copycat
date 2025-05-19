using UnityEngine;

namespace BachelorProject.Scenes
{
    public class SceneLoadCall : MonoBehaviour
    {
        public ObjectIdentifier gameSystems;

        public void LoadScene(StringVariable sceneName)
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