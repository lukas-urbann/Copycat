using UnityEngine;

namespace BachelorProject.Actions
{
    public class RandomRotationScale : MonoBehaviour
    {
        public Vector3 minRotation = new Vector3(0, 0, 0);
        public Vector3 maxRotation = new Vector3(360, 360, 360);

        public float minScale = 0.8f;
        public float maxScale = 1.2f;

        private void Start()
        {
            float randomX = Random.Range(minRotation.x, maxRotation.x);
            float randomY = Random.Range(minRotation.y, maxRotation.y);
            float randomZ = Random.Range(minRotation.z, maxRotation.z);
            transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);

            float randomScale = Random.Range(minScale, maxScale);
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }
}
