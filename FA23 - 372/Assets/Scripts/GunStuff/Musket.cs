using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;

public class Musket : MonoBehaviour {
    [SerializeField] InputManager inputManager;
    [SerializeField] PlayerMovement pm;
    [SerializeField] Transform cam;
    [Header("Gun Stuff (No need to touch)")]
    [SerializeField] 
    public GunState gState;
    [SerializeField] 
    public ReloadingState rState;
    [SerializeField] 
    public int bullets, tamps;
    [SerializeField] 
    public float Powder { get; private set; }
    [SerializeField] public float maxPowder, minPowder, sweetMin, sweetMax;
    private float movingPow = 3, stillPow = 1, camMovePow = 2f;
    private float lastCamRotation;
    private int bulletsToAdd;
    [Header("UI Stuff")]
    [SerializeField] TextMeshProUGUI bulletsText;
    [SerializeField] TextMeshProUGUI powderText;
    [SerializeField] TextMeshProUGUI tampsText;
    [SerializeField] TextMeshProUGUI gunStateText;
    [SerializeField] TextMeshProUGUI reloadingStateText; //Debugging text
    [Header("Bullet Stuff")]
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed;
    private void Start() {
        gState = GunState.READYTOFIRE;
        bullets = 1;
        TextUpdate();
        maxPowder = 0.66f;
        minPowder = 0.33f;
        sweetMin = 0.47f;
        sweetMax = 0.53f;
    }

    private void FixedUpdate() {
        lastCamRotation = cam.rotation.x;
    }

    private void Update() {
        if (pm.currentState == PlayerMovement.MovementState.moving || pm.currentState == PlayerMovement.MovementState.dashing) {
            sweetMax = 0.51f;
            sweetMin = 0.49f; //If moving tighten the sweet spot
        }
        else {
            sweetMin = 0.47f;
            sweetMax = 0.53f;
        }
        switch (gState) {
            case GunState.READYTOFIRE: //When reload is complete and the gun has bullets
                if (inputManager.Gun_Shoot == InputButtonState.ButtonDown) {
                    Shoot(); //Shoots the gun
                }
                break;
            case GunState.NOTREADY: //For failed reloads as well as after the gun is fired
                if (inputManager.Gun_Reload == InputButtonState.ButtonDown) {
                    gState = GunState.RELOADING;
                    rState = ReloadingState.RELOADINGSTAGE1;
                    tamps = 0;
                    Powder = 0f;
                    TextUpdate();
                }
                break;
            case GunState.RELOADING: //For the different stages of reloading
                switch (rState) {
                    case ReloadingState.RELOADINGSTAGE1: //Putting powder in the gun (between 1 and 2 seconds of holding f)
                        if (inputManager.Gun_Powder == InputButtonState.ButtonHeld) { //For adding powder to the gun
                            if (pm.currentState == PlayerMovement.MovementState.still) { //if the player is still you can fill powder super fast
                                if (cam.rotation.x != lastCamRotation) { //checks for camera movement that also inhibits pouring
                                    Powder += Time.deltaTime / (stillPow * camMovePow);
                                }
                                else {
                                    Powder += Time.deltaTime / stillPow;
                                }
                            }
                            else if (pm.currentState == PlayerMovement.MovementState.moving) { //if the player is moving it slows the reloading by quite a bit
                                if (cam.rotation.x != lastCamRotation) { //checks for camera movement that also inhibits pouring
                                    Powder += Time.deltaTime / (movingPow * camMovePow);
                                }
                                else {
                                    Powder += Time.deltaTime / movingPow;
                                }
                            }
                            else { //dashing
                                //powder doesn't fill at all
                            }
                             
                            TextUpdate();
                        }
                        else if (inputManager.Gun_Powder == InputButtonState.ButtonUp) { //When shift is released check to see if there's enough powder in the gun
                            if (Powder <= sweetMax && Powder >= sweetMin) {
                                rState = ReloadingState.RELOADINGSTAGE2;
                                bulletsToAdd = 3; //If in sweet spot add 3 bullets instead of 1
                                TextUpdate();
                            }
                            else if(Powder <= maxPowder && Powder >= minPowder) {
                                rState = ReloadingState.RELOADINGSTAGE2;
                                bulletsToAdd = 1;
                                TextUpdate();
                            }
                            else {
                                gState = GunState.NOTREADY; //if wrong ammount of poweder is in the gun change gState to NOTREADY
                                TextUpdate();
                            }
                        }
                        break;
                    case ReloadingState.RELOADINGSTAGE2: //Putting bullet in gun (one press of e)
                        if (inputManager.Gun_Bullet == InputButtonState.ButtonDown) {
                            bullets += bulletsToAdd; //Adds the bullet to the gun
                            rState = ReloadingState.RELOADINGSTAGE3;
                            TextUpdate();
                        }
                        break;
                    case ReloadingState.RELOADINGSTAGE3: //Tamping contents down in gun (three presses of q)
                        if(tamps < 3) {
                            if (inputManager.Gun_Tamp == InputButtonState.ButtonDown) {
                                tamps++;
                                TextUpdate();
                            }
                        }
                        else {
                            gState = GunState.READYTOFIRE;
                            tamps = 0;
                            Powder = 0f;
                            TextUpdate();
                        }
                        break;
                }
                break;
        }
        
    }

    private void Shoot() { // Shoots the gun and changes state to NOTREADY so the player needs to reload to fire again
        gState = GunState.NOTREADY;
        bullets--;
        TextUpdate();
        GameObject bullet = BulletPool.instance.GetPooledObjects();
        if (bullet != null) {
            bullet.SetActive(true);
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed; //Gives bullet velocity based on bulletSpeed
        }
        if (bullets > 0) {
            Invoke("Shoot", 0.15f);
        }
    }

    private void TextUpdate() { //Updates all the UI for debugging in the scene
        tampsText.text = "Tamps: " + tamps;
        powderText.text = "Powder: " + Mathf.Round(Powder*100)/100;
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
