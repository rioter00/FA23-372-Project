using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour{
    [SerializeField] InputManager input;
    [SerializeField] GameObject player;
    [SerializeField] GameObject menu;
    [SerializeField] GameManager gm;
    
    private void Update() {
        if(input.Escape == InputButtonState.ButtonDown) {
            if (menu.activeInHierarchy) {
                menu.SetActive(false);
                player.SetActive(true);
            }
            else {
                menu.SetActive(true);
                player.SetActive(false);
            }
            gm.ToggleCursorLock();
            //stop time
        }
    }
}
