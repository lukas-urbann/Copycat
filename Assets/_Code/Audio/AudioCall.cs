using System;
using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Audio
{
    [Serializable]
    public class AudioCall
    {
        public ObjectIdentifier audioSourceIdentifier;
        public List<AudioClip> possibleClips;
        private AudioSource cachedAudioSource = null;

        public void Play()
        {
            if (!cachedAudioSource && audioSourceIdentifier)
                audioSourceIdentifier.GetSourceComponent(out cachedAudioSource);

            if (!cachedAudioSource)
            {
                Debug.LogWarning($"{GetType().Name} nemá audio source!");
                return;
            }

            if (possibleClips == null || possibleClips.Count == 0)
            {
                return;
            }

            AudioClip selectedClip = possibleClips[UnityEngine.Random.Range(0, possibleClips.Count)];
            cachedAudioSource.PlayOneShot(selectedClip);
        }
    }
}