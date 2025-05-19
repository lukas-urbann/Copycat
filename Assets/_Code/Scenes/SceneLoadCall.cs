using UnityEngine;

namespace BachelorProject.Scenes
{
    public class SceneLoadCall : MonoBehaviour
    {
        public ObjectIdentifier gameSystems;

        public void LoadScene(StringVariable sceneName)
        {
            gameSystems.GetSourceComponent<SceneLoader>().LoadSceneCall(sceneName);
        }
    }
}