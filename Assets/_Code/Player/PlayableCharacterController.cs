using UnityEngine;
using UnityEngine.InputSystem;

namespace BachelorProject.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayableCharacterInput))]
    public class PlayableCharacterController : MonoBehaviour
    {

        #region Movement Control

        [Header("Movement Control")]

        [SerializeField] private bool enableMove = true;
        [SerializeField] private bool enableLook = true;
        [SerializeField] private bool enableJump = true;

        #endregion

        #region Movement Speed

        [Header("Movement Speed")]

        [SerializeField] private FloatReference moveSpeed;
        [SerializeField] private FloatReference gravitySpeed;
        [SerializeField] private FloatReference jumpSpeed;
        [SerializeField] private FloatReference lookSpeed;

        #endregion

        #region Movement Variables

        [Header("Movement Variables")]

        [SerializeField] private FloatReference lookClamp;
        [SerializeField] private FloatReference lookSmoothing;
        [SerializeField] private FloatReference moveSmoothing;

        //Private variables
        private float rotationX = 0f;
        private Vector3 velocity;
        private Vector3 moveDirection;
        private Vector3 actualMove = Vector3.zero;
        private Vector3 desiredMove = Vector3.zero;

        #endregion

        #region Properties

        [SerializeField] private Animator playerCameraAnimator;
        [SerializeField] private Transform playerCameraRotator;
        [SerializeField] private PlayableCharacterInput playerInput { get; set; }
        [SerializeField] private CharacterController characterController { get; set; }

        #endregion

        #region Public Accessors

        public PlayableCharacterInput PlayerInput => playerInput;
        public CharacterController CharacterController => characterController;

        #endregion

        private void Start()
        {
            if (TryGetComponent(out PlayableCharacterInput input))
            {
                playerInput = input;
            }
            else
            {
                Debug.LogError("Hráè nemá input!");
            }

            if (TryGetComponent(out CharacterController controller))
            {
                characterController = controller;
            }
            else
            {
                Debug.LogError($"Hráè nemá {typeof(CharacterController)}!");
            }

            GetControls();
            Helper.CursorStates.LockCursor();
        }

        private void Update()
        {
            Move();
            Look();
        }

        private void GetControls()
        {
            if (playerInput == null)
            {
                Debug.LogError("PlayerInput component is null. Cannot bind input actions.");
                return;
            }

            if (playerInput.JumpAction != null)
            {
                playerInput.JumpAction.performed += Jump;
            }
            else
            {
                Debug.LogWarning("Jump action is null. Jump functionality will not work.");
            }

            if (playerInput.PauseAction != null)
            {
                playerInput.PauseAction.performed += (InputAction.CallbackContext ctx) => { Helper.CursorStates.UnlockCursor(); };
            }
            else
            {
                Debug.LogWarning("Pause action is null. Pause functionality will not work.");
            }
        }

        private void Jump(InputAction.CallbackContext ctx)
        {
            if (!enableJump) return;

            if (!CharacterController.isGrounded) return;

            velocity.y = jumpSpeed.Value;
            playerCameraAnimator.SetTrigger("Jump");
        }

        private Vector2 currentMouseDelta;
        private void Look()
        {
            if (!enableLook) return;

            Vector2 mouseInput = new(PlayerInput.Look.x, PlayerInput.Look.y);
            currentMouseDelta = Vector2.Lerp(currentMouseDelta, mouseInput, 1f / lookSmoothing.Value);

            rotationX -= currentMouseDelta.y * lookSpeed.Value * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -lookClamp.Value, lookClamp.Value);

            playerCameraRotator.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
            transform.Rotate(currentMouseDelta.x * lookSpeed.Value * Time.deltaTime * Vector3.up);
        }

        private void Move()
        {
            if (!enableMove) return;

            velocity.y = (CharacterController.isGrounded && velocity.y < 0) ? -2f : velocity.y;

            // Pohyb
            moveDirection = transform.right * PlayerInput.Movement.x + transform.forward * PlayerInput.Movement.y;
            desiredMove = new(moveDirection.x, 0, moveDirection.z);
            desiredMove *= moveSpeed.Value;
            actualMove = Vector3.Lerp(actualMove, desiredMove, Time.deltaTime * moveSmoothing.Value);
            CharacterController.Move(actualMove * Time.deltaTime);

            // Gravitace
            velocity.y += -gravitySpeed.Value * Time.deltaTime;
            CharacterController.Move(velocity * Time.deltaTime);

            // Animace
            playerCameraAnimator.SetFloat("MoveSpeed", actualMove.magnitude);
            playerCameraAnimator.SetBool("IsGrounded", CharacterController.isGrounded);
        }
    }
}