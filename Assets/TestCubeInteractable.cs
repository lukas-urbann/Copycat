using UnityEngine;

namespace BachelorProject
{
    public class TestCubeInteractable : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Ahoj");
        }

        public void OnSee()
        {
            Debug.Log("Ahoj2");
        }

        public void OnUnsee()
        {
            Debug.Log("Ahoj3");
        }
    }
}