namespace BachelorProject
{
    /// <summary>
    /// Zaklad pro vsechny interakce s objekty
    /// </summary>
    public interface IInteractable
    {
        bool IsInteractable { get; }

        void Interact();

        void OnSee();

        void OnUnsee();
    }
}
