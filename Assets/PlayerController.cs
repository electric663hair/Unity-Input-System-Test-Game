// using System.Threading;
using UnityEngine;
// using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
// using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject moveStickUI;
    [SerializeField] private GameObject lookStickUI;
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject PauseMenu;

    private CharacterController characterController;
    private PlayerInput playerInput;
    private Camera playerCamera;
    PauseController pauseController;
    // private PlayerControls playerControls;

    private bool isGrounded;
    private bool isSprinting;
    private bool invertedY;

    private float moveSpeed = 5f;
    private float walkSpeed;
    private float sprintSpeed;
    private float xRotation = 0f;
    private float gravity = -20f;
    private float jumpStrength = 2f;
    private float groundCheckDistance = .2f;
    private float joystickUIMultiplier = 20f;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private Vector3 moveDirection;
    private Vector3 smoothMoveVelocity;
    private Vector3 velocity;

    private Transform groundCheck;

    private void Awake() {
        pauseController = PauseMenu.GetComponent<PauseController>();
        groundCheck = transform.Find("GroundCheck");
        characterController = transform.GetComponent<CharacterController>();

        walkSpeed = moveSpeed;
        sprintSpeed = moveSpeed * 1.5f;

        // playerControls = new PlayerControls();
        playerInput = transform.GetComponent<PlayerInput>();

        playerCamera = transform.GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDrawGizmos() {
        if (groundCheck) {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
            Gizmos.color = Color.magenta;
        }
    }

    private void Update() {

        if (transform.position.y < -50) {
            transform.position = new Vector3(0, 1, 0);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, ~playerMask);

        UpdateGamepadUI();
        if (!pauseController.gamePaused) {
            MovePlayer();
            MoveCamera();
            PlayerPhysics();
        }

        Debug.Log(playerInput.currentControlScheme);
    }

    private void OnJump(InputValue inputValue) {
        if (isGrounded && !pauseController.gamePaused) {
            velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);
        }
    }

    private void OnEscape() {
        if (pauseController) {
            pauseController.TogglePauseMenu();
        } else {
            Debug.LogError("PauseController not found in scene");
        }
    }

    private void OnCrouch() {
        Debug.Log("Crouch");
    }

    private void OnSprint() {
        isSprinting = true;
    }

    private void OnMove(InputValue inputValue) {
        moveInput = inputValue.Get<Vector2>();
    }

    private void MovePlayer() {

        Vector3 desiredMove = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (new Vector3(moveDirection.x, 0, moveDirection.z).magnitude < 0.1f) {
            isSprinting = false;
        }

        if (isSprinting && isGrounded) {
            moveSpeed = sprintSpeed;
        } else {
            moveSpeed = walkSpeed;
        }

        moveDirection = Vector3.SmoothDamp(moveDirection, desiredMove.normalized * moveSpeed, ref smoothMoveVelocity, smoothTime);
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void PlayerPhysics() {
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnLook(InputValue inputValue) {
        lookInput = inputValue.Get<Vector2>();
    }
    private void MoveCamera() {
        float mouseSensChange;
        float currentSens;
        if (playerInput.currentControlScheme == "Keyboard&Mouse") {
            mouseSensChange = .05f;
            currentSens = Inputs.mouseSens;
        } else {
            mouseSensChange = 1;
            currentSens = Inputs.controllerSens;
        }

        float mouseX = lookInput.x * currentSens * 4f * mouseSensChange;
        float mouseY = lookInput.y * currentSens * 2.5f * mouseSensChange;

        if (invertedY) {
            mouseY *= -1f;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void UpdateGamepadUI() {
        Vector2 moveStickMove = moveInput; //new Vector2(Mathf.Clamp(moveInput.x, -90, 90), Mathf.Clamp(moveInput.x, -90, 90));
        Vector2 lookStickMove = Vector2.zero;
        if (playerInput.currentControlScheme == "Keyboard&Mouse") {
            lookStickMove = new Vector2(Mathf.Clamp(Mathf.Clamp(lookInput.x, -90, 90) / 25, -.9f, .9f), Mathf.Clamp(Mathf.Clamp(lookInput.y, -90, 90) / 25, -.9f, .9f));
        } else if (playerInput.currentControlScheme == "Gamepad") {
            lookStickMove = lookInput;
        }

        moveStickUI.transform.GetChild(0).localPosition = moveStickMove * joystickUIMultiplier;
        lookStickUI.transform.GetChild(0).localPosition = lookStickMove * joystickUIMultiplier;
    }
}