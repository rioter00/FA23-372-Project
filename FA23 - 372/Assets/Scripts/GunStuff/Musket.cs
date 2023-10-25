using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Musket : MonoBehaviour{
    [Header("Gun Stuff (No need to touch)")]
    [SerializeField] GunState gState;
    [SerializeField] ReloadingState rState;
    [SerializeField] int bullets, tamps;
    [SerializeField] float powder;
    [Header("UI Stuff")]
    [SerializeField] TextMeshProUGUI bulletsText;
    [SerializeField] TextMeshProUGUI powderText;
    [SerializeField] TextMeshProUGUI tampsText;
    [SerializeField] TextMeshProUGUI gunStateText;
    [SerializeField] TextMeshProUGUI reloadingStateText; //Debugging text
    [Header("Bullet Stuff")]
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    private void Start() {
        gState = GunState.READYTOFIRE;
        bullets = 1;
        TextUpdate();
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
                    TextUpdate();
                }
                break;
            case GunState.RELOADING: //For the different stages of reloading
                switch (rState) {
                    case ReloadingState.RELOADINGSTAGE1: //Putting powder in the gun (between 1 and 2 seconds of holding f)
                        if (Input.GetKey(KeyCode.F)) {
                            powder += Time.deltaTime; //For adding powder to the gun
                            TextUpdate();
                        }
                        else if (Input.GetKeyUp(KeyCode.F)) { //When f is released check to see if there's enough powder in the gun
                            if(powder <= 2f && powder >= 1f) {
                                rState = ReloadingState.RELOADINGSTAGE2;
                                TextUpdate();
                            }
                            else {
                                gState = GunState.NOTREADY; //if wrong ammount of poweder is in the gun change gState to NOTREADY
                                TextUpdate();
                            }
                        }
                        break;
                    case ReloadingState.RELOADINGSTAGE2: //Putting bullet in gun (one press of e)
                        if (Input.GetKeyDown(KeyCode.E)) {
                            bullets++; //Adds the bullet to the gun (can be changed later to add special reload cases if wanted)
                            rState = ReloadingState.RELOADINGSTAGE3;
                            TextUpdate();
                        }
                        break;
                    case ReloadingState.RELOADINGSTAGE3: //Tamping contents down in gun (three presses of c)
                        if(tamps < 3) {
                            if (Input.GetKeyDown(KeyCode.C)) {
                                tamps++;
                                TextUpdate();
                            }
                        }
                        else {
                            gState = GunState.READYTOFIRE;
                            tamps = 0;
                            powder = 0f;
                            TextUpdate();
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
        TextUpdate();
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation); //Spawns bullet at bulletSpawnPoint
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed; //Gives bullet velocity based on bulletSpeed
    }

    private void TextUpdate() { //Updates all the UI for debugging in the scene
        tampsText.text = "Tamps: " + tamps;
        powderText.text = "Powder: " + Mathf.Round(powder*100)/100;
        bulletsText.text = "Bullets: " + bullets;
        reloadingStateText.text = rState.ToString();
        gunStateText.text = gState.ToString();
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
