using BachelorProject.Audio;
using UnityEngine;

namespace BachelorProject.Player
{
    public class PlayableCharacterFootsteps : MonoBehaviour
    {
        public AudioCall footstepsAudio;
        public AudioCall jumpAudio;

        public void PlayFootstep()
        {
            footstepsAudio.Play();
        }

        public void PlayJump()
        {
            jumpAudio.Play();
        }
    }
}
