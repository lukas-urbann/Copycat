using UnityEngine;

namespace BachelorProject.Audio
{
    public class AudioSystem : MonoBehaviour
    {
        public ObjectIdentifier globalAudioSourceIdentifier;

        private AudioSource cachedAudioSource;

        private void Start()
        {
            GetAudioSource();
        }

        public void GetAudioSource()
        {
            globalAudioSourceIdentifier.GetSourceComponent(out cachedAudioSource);
        }

        public void PlayAudioClip(AudioClip clip)
        {
            cachedAudioSource.PlayOneShot(clip);
        }
    }
}
