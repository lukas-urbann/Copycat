using UnityEngine;

namespace BachelorProject.Audio
{
    /// <summary>
    /// Třída pro držení odkazu na AudioCall. K použití v Unity editoru.
    /// </summary>
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
