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
        private float playerRotationX = 0f;
        private Vector3 velocity;
        private Vector3 moveDirection;
        private Vector3 actualMove = Vector3.zero;
        private Vector3 desiredMove = Vector3.zero;
        private Vector2 mouseDelta;
        #endregion

        #region Properties

        [SerializeField] private Animator playerCameraAnimator;
        [SerializeField] private Animator playerItemAnimator;
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
                Debug.LogError("Hr�� nem� input!");
            }

            if (TryGetComponent(out CharacterController controller))
            {
                characterController = controller;
            }
            else
            {
                Debug.LogError($"Hr�� nem� {typeof(CharacterController)}!");
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
            if (!playerInput)
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
        }

        private void Jump(InputAction.CallbackContext ctx)
        {
            if (!enableJump) return;
            if (!CharacterController.isGrounded) return;
            velocity.y = jumpSpeed.Value;
            playerCameraAnimator.SetTrigger("Jump");
            playerItemAnimator.SetTrigger("Jump");
        }

        private void Look()
        {
            if (!enableLook) return;

            Vector2 rawMouseInput = new(PlayerInput.Look.x, PlayerInput.Look.y);
            mouseDelta = Vector2.Lerp(mouseDelta, rawMouseInput, 1f / lookSmoothing.Value);

            playerRotationX -= mouseDelta.y * lookSpeed.Value * Time.deltaTime;
            playerRotationX = Mathf.Clamp(playerRotationX, -lookClamp.Value, lookClamp.Value);

            playerCameraRotator.localRotation = Quaternion.Euler(playerRotationX, 0f, 0f);
            transform.Rotate(mouseDelta.x * lookSpeed.Value * Time.deltaTime * Vector3.up);
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
            playerItemAnimator.SetFloat("MoveSpeed", actualMove.magnitude);
            playerItemAnimator.SetBool("IsGrounded", CharacterController.isGrounded);
        }
    }
}