//The variables below are not static.

inputManager.Movement ==> Vector2 for WASD keys
inputManager.Mouse ==> Vector2 for mouse movement
inputManager.Gun_Shoot ==> InputButtonState for shooting the gun (left click)
inputManager.Gun_Reload ==> InputButtonState for reloading the gun (R)
inputManager.Gun_Powder ==> InputButtonState for adding powder (F)
inputManager.Gun_Bullet ==> InputButtonState for adding bullets (E)
inputManager.Gun_Tamp ==> InputButtonState for tamping the gun (C)




InputButtonStates possible values: [ ButtonNotHeld, ButtonDown, ButtonHeld, ButtonUp ]

ButtonDown ==> The FIRST FRAME that the button IS being held for.
ButtonUp ==> The FIRST FRAME that the button is NOT being held for.
ButtonHeld ==> Every frame that the button IS being held for (except frame 1).
ButtonNotHeld ==> Every frame that the button is NOT being held for (except frame 1).




Properly implementing InputManager
==================================
//This applies to InputButtonStates only. Movement and Mouse are just Vector2s.

(Input.GetKeyDown(KeyCode.Mouse0)) ==> (inputManager.Gun_Shoot == InputButtonState.ButtonDown)

(Input.GetKey(KeyCode.F)) ==> (inputManager.Gun_Powder == InputButtonState.ButtonHeld)
//Note that the line above will not count the first frame of the button being held. This can be solved by adding an OR operator for both InputButtonState.ButtonHeld and for InputButtonState.ButtonDown.