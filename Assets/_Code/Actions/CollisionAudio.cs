using BachelorProject.Audio;
using UnityEngine;

namespace BachelorProject.Actions
{
    /// <summary>
    /// Schopnost pro přehrání zvuku při kolizi.
    /// </summary>
    public class CollisionAudio : MonoBehaviour
    {
        public AudioCall collisionAudio;

        public void OnCollisionEnter(Collision collision)
        {
            collisionAudio.Play();
        }
    }
}
