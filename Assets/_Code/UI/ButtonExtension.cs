using BachelorProject.Audio;
using UnityEngine;

namespace BachelorProject.Actions
{
    public class ButtonExtension : MonoBehaviour
    {
        public AudioCall buttonHover;
        public AudioCall buttonClick;

        public void PlayHover() => buttonHover.Play();
        public void PlayClick() => buttonClick.Play();
    }
}