using BachelorProject.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BachelorProject.Player
{
    public class UIInput : MonoBehaviour
    {
        private GameControls InputActions { get; set; }
        private InputAction PauseAction { get; set; }
        
        public BoolReference isPaused;
        public ObjectIdentifier menuScreen;
        public BoolEvent onPauseEvent;
        
        private void Awake()
        {
            InputActions = new GameControls();
            AssignActions();
            isPaused.Variable.Value = false;
        }

        private void OnEnable() => InputActions.Enable();

        private void OnDisable() => InputActions.Disable();

        private void AssignActions()
        {
            PauseAction = InputActions.UI.Pause;
        }

        private void Start()
        {
            AssignPause();
        }
        
        private void AssignPause()
        {
            if (PauseAction != null)
            {
                PauseAction.performed += SwitchPauseState;
            }
            else
            {
                Debug.LogWarning("Pause action je null");
            }
        }

        private void SwitchPauseState(InputAction.CallbackContext ctx)
        {
            isPaused.Variable.Value = !isPaused.Variable.Value;
            
            if (!isPaused.Variable.Value)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void MenuPauseReset(bool inGame)
        {
            if (inGame) return;
            Time.timeScale = 1;
            isPaused.Variable.Value = false;
            Helper.CursorStates.UnlockCursor();
            menuScreen.GetSourceObject()?.SetActive(true);
        }
        
        public void PauseGame()
        {
            isPaused.Variable.Value = true;
            onPauseEvent?.Execute();
            Helper.CursorStates.UnlockCursor();
            menuScreen.GetSourceObject()?.SetActive(true);
            
            Time.timeScale = 0;
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;

            isPaused.Variable.Value = false;
            onPauseEvent?.Execute();
            Helper.CursorStates.LockCursor();
            menuScreen.GetSourceObject()?.SetActive(false);
        }
    }
}