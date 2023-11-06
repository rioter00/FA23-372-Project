using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public MovementState currentState = MovementState.still;
    public enum MovementState
    {
        still, moving, dashing
    }

    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [Range(0, 1)] [SerializeField] private float movementDampTime = 0.5f;
    [Range(0, 1)] [SerializeField] private float cameraDampTime = 0.5f;
    [SerializeField] private bool cursorLock = true;
    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0, -1, 0);
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float dashSpeedMultiplier = 3;

    private CharacterController controller;

    private Vector2 movementInput;
    private Vector2 movementInputVel;

    private Vector2 mouseInput;
    private Vector2 mouseInputVel;

    private Transform playerCam;
    private float cameraCap;

    private InputManager input;

    private Vector3 groundCheck;
    private bool isGrounded;
    private float velocityY;

    private bool dashReady = true;
    private float baseMovementSpeed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCam = Camera.main.transform;
        input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        input.OnDashButtonDown.AddListener(Dash);
        baseMovementSpeed = movementSpeed;
        if (cursorLock) Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        UpdateMoveInput();
        UpdateMouseInput();
    }

    private void FixedUpdate()
    {
        groundCheck = transform.position + groundCheckOffset;
    }

    private void UpdateMoveInput()
    {
        isGrounded = Physics.CheckSphere(groundCheck, 0.2f, groundMask);
        Vector2 targetMovementInput = input.Movement;
        UpdateMovementState(targetMovementInput);
        movementInput = Vector2.SmoothDamp(movementInput, targetMovementInput, ref movementInputVel, movementDampTime);
        if (!isGrounded) velocityY += gravity * 2f * Time.deltaTime;
        else if (isGrounded && velocityY < 0) velocityY = 0;
        Vector3 velocity = (transform.forward * movementInput.y + transform.right * movementInput.x) * movementSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    private void UpdateMovementState(Vector2 targetMovementInput)
    {
        if (targetMovementInput == Vector2.zero) currentState = MovementState.still;
        else if (movementSpeed != baseMovementSpeed) currentState = MovementState.dashing;
        else currentState = MovementState.moving;
    }

    private void UpdateMouseInput()
    {
        Vector2 targetMouseInput = input.Mouse;
        mouseInput = Vector2.SmoothDamp(mouseInput, targetMouseInput, ref mouseInputVel, cameraDampTime);
        cameraCap -= mouseInput.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90f, 90f);
        playerCam.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * mouseInput.x * mouseSensitivity);
    }

    private void Dash()
    {
        Debug.Log("dash");
        if (dashReady) StartCoroutine(_dash());
    }

    private IEnumerator _dash()
    {
        dashReady = false;
        movementSpeed *= dashSpeedMultiplier;
        yield return new WaitForSeconds(dashDuration);
        movementSpeed /= dashSpeedMultiplier;
        yield return new WaitForSeconds(dashCooldown);
        dashReady = true;
    }
}
