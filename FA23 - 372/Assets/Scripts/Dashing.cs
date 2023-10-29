using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashDuration;


    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVel = true;
    float horizontalInput;
    float verticalInput;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    //dash function
    private void Dash()
    {
        //cooldown timer can be changed in Unity editor
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        Transform forwardT;

        if (useCameraForward)
        {
            forwardT = playerCam;
        }
        else
        {
            forwardT = orientation;
        }
        Vector3 direction = GetDirection(forwardT);
        Vector3 forceToApply = direction * dashForce;

        if (disableGravity)
            rb.useGravity = forwardT;


        delayedForceToApply = forceToApply;
        Invoke(nameof(ResetDash), dashDuration);
        Invoke(nameof(DelayedDashForce), 0.025f);
        //pm.dashing = true;

    }


    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
        if (resetVel)
            rb.velocity = Vector3.zero;
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    //reseting dash after pressing dash
    private void ResetDash()
    {
        //pm.dashing = false;

        if (disableGravity)
            rb.useGravity = true;

    }

    //direction in which player dashes
    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;
        return direction.normalized;
    }
}
