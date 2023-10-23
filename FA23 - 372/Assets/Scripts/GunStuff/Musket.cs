using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musket : MonoBehaviour{
    [SerializeField] GunState gState;
    [SerializeField] ReloadingState rState;
    [SerializeField] int bullets, tamps;
    [SerializeField] float powder;
    private void Start() {
        gState = GunState.READYTOFIRE;
        bullets = 1;
    }

    private void Update() {
        switch (gState) {
            case GunState.READYTOFIRE: //When reload is complete and the gun has bullets
                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    Shoot(); //Shoots the gun
                }
                break;
            case GunState.NOTREADY: //For failed reloads as well as after the gun is fired
                if (Input.GetKeyDown(KeyCode.R)) {
                    gState = GunState.RELOADING;
                    rState = ReloadingState.RELOADINGSTAGE1;
                    tamps = 0;
                    powder = 0f;
                }
                break;
            case GunState.RELOADING: //For the different stages of reloading
                switch (rState) {
                    case ReloadingState.RELOADINGSTAGE1: //Putting powder in the gun (between 1 and 2 seconds of holding f)
                        if (Input.GetKey(KeyCode.F)) {
                            powder += Time.deltaTime; //For adding powder to the gun
                        }
                        else if (Input.GetKeyUp(KeyCode.F)) { //When f is released check to see if there's enough powder in the gun
                            if(powder <= 2f && powder >= 1f) {
                                rState = ReloadingState.RELOADINGSTAGE2;
                            }
                            else {
                                gState = GunState.NOTREADY; //if wrong ammount of poweder is in the gun change gState to NOTREADY
                            }
                        }
                        break;
                    case ReloadingState.RELOADINGSTAGE2: //Putting bullet in gun (one press of e)
                        if (Input.GetKeyDown(KeyCode.E)) {
                            bullets++; //Adds the bullet to the gun (can be changed later to add special reload cases if wanted)
                            rState = ReloadingState.RELOADINGSTAGE3;
                        }
                        break;
                    case ReloadingState.RELOADINGSTAGE3: //Tamping contents down in gun (three presses of c)
                        if(tamps < 3) {
                            if (Input.GetKeyDown(KeyCode.C)) {
                                tamps++;
                            }
                        }
                        else {
                            gState = GunState.READYTOFIRE;
                        }
                        break;
                }
                break;
        }
        if(gState == GunState.READYTOFIRE && Input.GetKeyDown(KeyCode.Mouse0)) {
            Shoot();
        }
        
    }

    private void Shoot() { // Shoots the gun and changes state to NOTREADY so the player needs to reload to fire again
        gState = GunState.NOTREADY;
        bullets--;
        //Other shooting stuff
    }
}
public enum GunState { //Keeps track of the state of the gun for further use with Enemy AI or other things
    READYTOFIRE,
    NOTREADY,
    RELOADING,
}

public enum ReloadingState { //Keeps track of what state the player is in the reloading sequence
    RELOADINGSTAGE1,
    RELOADINGSTAGE2,
    RELOADINGSTAGE3,
}
