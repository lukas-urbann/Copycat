using BachelorProject.Audio;
using BachelorProject.Management;
using BachelorProject.Player;
using UnityEngine;

namespace BachelorProject.Interactable
{
    /// <summary>
    /// Skript pro klice, klice mohou byt pouzity na dvere.
    /// Dedi z GrabbableInteractable, aby se daly uchopit do ruky hrace.
    /// Mohou byt "zkonzumovany" dvermi
    /// </summary>
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
