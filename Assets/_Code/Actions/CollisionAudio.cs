using BachelorProject.Audio;
using UnityEngine;

namespace BachelorProject.Actions
{
    public class CollisionAudio : MonoBehaviour
    {
        public AudioCall collisionAudio;

        public void OnCollisionEnter(Collision collision)
        {
            collisionAudio.Play();
        }
    }
}
