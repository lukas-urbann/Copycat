namespace BachelorProject
{
    public interface IInteractable
    {
        bool IsInteractable { get; }

        void Interact();

        void OnSee();

        void OnUnsee();
    }
}
