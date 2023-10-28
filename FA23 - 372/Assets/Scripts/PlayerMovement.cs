using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float groundDrag;
    public float dashSpeed;
    public float dashSpeedChangeFactor;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;
    public Transform orientation;



    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    public MovementState state;
    public enum MovementState
    {
        dashing,
    }
    public bool dashing;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    private void StateHander()
    {
        if (dashing)
        {

            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
        }


        bool desiredMoveSpeedhasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedhasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                movementSpeed = desiredMoveSpeed;
            }
        }
        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private float speedChangeFactor;
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - movementSpeed);
        float startValue = movementSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            movementSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        movementSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        MyInput();
        SpeedControl();
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;


    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);

    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

}
