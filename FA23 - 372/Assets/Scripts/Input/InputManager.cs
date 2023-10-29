using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public enum InputButtonState { ButtonNotHeld, ButtonDown, ButtonHeld, ButtonUp };

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool logInputs;
    private PlayerInputActions playerControls;

    [SerializeField] public Vector2 Movement;
    [SerializeField] public Vector2 Mouse;
    [SerializeField] public InputButtonState Gun_Shoot;
    [SerializeField] public InputButtonState Gun_Reload;
    [SerializeField] public InputButtonState Gun_Powder;
    [SerializeField] public InputButtonState Gun_Bullet;
    [SerializeField] public InputButtonState Gun_Tamp;
    [SerializeField] public InputButtonState Dash;

    [HideInInspector] public UnityEvent OnDashButtonDown;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;   
        //leaving this out for not as im not sure it should be handled here or in the proper script for it
        //ideally this should be enabled/disabled depending on the currently active input map, which i supposed would be handled here.

        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerControls.gameplay.Enable();
    }
    private void OnDisable()
    {
        playerControls.gameplay.Disable();
    }

    private void Update()
    {
        Movement = playerControls.gameplay.Movement.ReadValue<Vector2>();
        Mouse = playerControls.gameplay.Mouse.ReadValue<Vector2>();

        Gun_Shoot = UpdateButtonState(Gun_Shoot, playerControls.gameplay.Gun_Shoot);
        Gun_Reload = UpdateButtonState(Gun_Reload, playerControls.gameplay.Gun_Reload);
        Gun_Powder = UpdateButtonState(Gun_Powder, playerControls.gameplay.Gun_Powder);
        Gun_Bullet = UpdateButtonState(Gun_Bullet, playerControls.gameplay.Gun_Bullet);
        Gun_Tamp = UpdateButtonState(Gun_Tamp, playerControls.gameplay.Gun_Tamp);
        Dash = UpdateButtonState(Dash, playerControls.gameplay.Dash);

        if (Dash == InputButtonState.ButtonDown) OnDashButtonDown.Invoke();

        //wasd = move
        //mouse = look
        //left click = shoot
        //r = reload
        //f = powder
        //e = add bullet
        //c = tamp

        //debug area
        /*
        if (Gun_Shoot == InputButtonState.ButtonDown)
        {
            Debug.Log("hi");
        }*/
    }

    private InputButtonState UpdateButtonState(InputButtonState buttonState, InputAction input)
    {
        if (Mathf.Approximately(input.ReadValue<float>(), 1f))
        {
            if (buttonState.Equals(InputButtonState.ButtonDown))
                buttonState = InputButtonState.ButtonHeld;
            else if (!buttonState.Equals(InputButtonState.ButtonHeld))
                buttonState = InputButtonState.ButtonDown;
        }
        else
        {
            if (buttonState.Equals(InputButtonState.ButtonUp))
                buttonState = InputButtonState.ButtonNotHeld;
            else if (!buttonState.Equals(InputButtonState.ButtonNotHeld))
                buttonState = InputButtonState.ButtonUp;
        }

        //log inputs
        if (logInputs)
            if (buttonState.Equals(InputButtonState.ButtonDown) || buttonState.Equals(InputButtonState.ButtonUp))
                Debug.Log(input.name + " " + buttonState.ToString());

        return buttonState;
    }

    public void EnableInput()
    {
        playerControls.gameplay.Enable();
    }
    public void DisableInput()
    {
        playerControls.gameplay.Disable();
    }
}
