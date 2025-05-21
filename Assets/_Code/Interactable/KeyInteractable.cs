using BachelorProject.Audio;
using BachelorProject.Management;
using BachelorProject.Player;
using UnityEngine;

namespace BachelorProject.Interactable
{
    public class KeyInteractable : GrabbableInteractable
    {
        [Header("Key Interactable")]
        public StringReference KeyID;
        public AudioCall consumeAudio;

        public override void Use()
        {
            Debug.Log($"Key '{KeyID.Value}': Use");
        }

        public void Consume()
        {
            if (!ObjectRegistry.GetObject(playerRoot, out var go)) return;
            consumeAudio.Play();
            go.GetComponent<PlayableCharacterHand>().ReleaseItem();
            Destroy(gameObject);
        }
    }
}
