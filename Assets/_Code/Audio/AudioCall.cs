using System;
using System.Collections.Generic;
using UnityEngine;

namespace BachelorProject.Audio
{
    [Serializable]
    public class AudioCall
    {
        [Header("Identifikátor, kde se hledá audio source")]
        public ObjectIdentifier audioSourceIdentifier;
        [Header("Audio Source ze kterého se zvuk hraje")]
        public AudioSource cachedAudioSource = null;
        public List<AudioClip> possibleClips;

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