using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IHealth{
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public LivingState LState { get; set; }
    private bool canRegan = false;
    private int regenRate = 10;
    private bool isRegen;
    private bool canTakeDamage = true;
    private float invTime = 1.5f;

    [Header("Add the Splatter image here")]
    [SerializeField] private Image redSplatterImage = null;

    [Header("Hurt Image Flash")]
    [SerializeField] private Image hurtImage = null;
    [SerializeField] private float hurtTimer = 0.1f;

    [Header("Heal Timer")]
    [SerializeField] private float healCooldown = 2.0f;
    [SerializeField] private float maxHealCooldown = 2.0f;
    [SerializeField] private bool startCooldown = false;


    [Header("Audio Name")]
    [SerializeField] private AudioClip hurtAudio = null;
    private AudioSource healthAudioSource;

    public void TakeDamage(int damage){
        if (canTakeDamage) {
            canTakeDamage = false;
            HP -= damage;
            if (HP <= 0) {
                Death();
            }
            else {
                canRegan = false;
                StartCoroutine(HurtFlash());
                UpdateHealth();
                healCooldown = maxHealCooldown;
                startCooldown = true;
            }
            Invoke("DamageReset", invTime);
        }
        
    }
    public void Death(){
        LState = LivingState.DEAD;
        //Do other death things with player
    }

    private void Start(){
        MaxHP = 100;
        HP = MaxHP;
        LState = LivingState.ALIVE;
        healthAudioSource = GetComponent<AudioSource>();
    }

    void UpdateHealth() {
        Color splatterAlpha = redSplatterImage.color;
        splatterAlpha.a = 1 - (HP / MaxHP);
        redSplatterImage.color = splatterAlpha;
    }

    IEnumerator HurtFlash() {
        hurtImage.enabled = true;
        healthAudioSource.PlayOneShot(hurtAudio);
        yield return new WaitForSeconds(hurtTimer);
        hurtImage.enabled = false;
    }

    private void Update() {
        if (startCooldown) {
            healCooldown -= Time.deltaTime;
            if (healCooldown <= 0) {
                canRegan = true;
                startCooldown = false;
            }
        }

        if (canRegan) {
            if (HP <= MaxHP - 1) {
                if (!isRegen) {
                    isRegen = true;
                    Invoke(nameof(Regen), 1);
                }
            }
            else {
                HP = MaxHP;
                healCooldown = maxHealCooldown;
                canRegan = false;
            }
        }
    }

    private void Regen() {
        if (HP < MaxHP) {
            HP += regenRate;
            UpdateHealth();
            isRegen = false;
        }
    }

    private void DamageReset() {
        canTakeDamage = true;
    }
}
