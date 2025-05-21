using UnityEngine;

namespace BachelorProject.Audio
{
    public class AudioCallHolder : MonoBehaviour
    {
        public AudioCall audioCall;

        public void PlayAudio()
        {
            if (audioCall != null)
            {
                audioCall.Play();
            }
            else
            {
                Debug.LogWarning("AudioCall is not assigned.");
            }
        }
    }
}
