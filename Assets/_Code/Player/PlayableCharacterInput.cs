using UnityEngine;
using UnityEngine.InputSystem;

namespace BachelorProject.Player
{
    public class PlayableCharacterInput : MonoBehaviour
    {
        private GameControls InputActions { get; set; }
        public InputAction JumpAction { get; private set; }
        public InputAction InteractAction { get; private set; }
        public InputAction DropAction { get; private set; }
        public Vector2 Movement => InputActions.Player.Move.ReadValue<Vector2>();
        public Vector2 Look => InputActions.Player.Look.ReadValue<Vector2>();

        private void Awake()
        {
            InputActions = new GameControls();
            AssignActions();
        }

        private void OnEnable() => InputActions.Enable();

        private void OnDisable() => InputActions.Disable();

        private void AssignActions()
        {
            JumpAction = InputActions.Player.Jump;
            InteractAction = InputActions.Player.Interact;
            DropAction = InputActions.Player.Drop;
        }
        
        public bool GetJump => InputActions.Player.Jump.ReadValue<float>() > 0;
        public bool GetInteraction => InputActions.Player.Interact.ReadValue<float>() > 0;
    }
}