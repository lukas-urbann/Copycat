namespace BachelorProject
{
    public interface IInteractable
    {
        // This method will be called when the player interacts with the object
        void Interact();

        // This method will be called when the player looks at the object
        void OnSee();

        // This method will be called when the player looks away from the object
        void OnUnsee();
    }
}
