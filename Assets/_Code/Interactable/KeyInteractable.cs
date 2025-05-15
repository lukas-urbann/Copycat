using BachelorProject.Player;
using UnityEngine;

namespace BachelorProject.Interactable
{
    public class KeyInteractable : GrabbableInteractable
    {
        public StringReference KeyID;

        public override void Use()
        {
            Debug.Log($"Key '{KeyID.Value}': Use");
        }

        public void Consume()
        {
            if (!ObjectRegistry.GetObject(playerRoot, out var go)) return;
            go.GetComponent<PlayableCharacterHand>().ReleaseItem();
            Destroy(gameObject);
        }
    }
}
