using BachelorProject.Player;

namespace BachelorProject.Interactable
{
    public class KeyInteractable : GrabbableInteractable
    {
        public StringReference KeyID;

        public override void Use()
        {
            if (!IdentifiableObjectRegistry.GetObject(playerRoot, out var go)) return;
            go.GetComponent<PlayableCharacterHand>().ReleaseItem();
            Destroy(gameObject);
        }
    }
}
