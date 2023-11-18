using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealth : MonoBehaviour, IHealth
{
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public LivingState LState { get; set; }

    public void TakeDamage(int damage) {
        HP -= damage;
        if (HP <= 0) {
            Death();
        }
    }
    public void Death() {
        LState = LivingState.DEAD;
        gameObject.SetActive(false);
        Invoke("Respawn", 3);
    }

    private void Start() {
        MaxHP = 1;
        HP = MaxHP;
        LState = LivingState.ALIVE;
    }

    private void Respawn() {
        gameObject.SetActive(true);
        LState = LivingState.ALIVE;
    }
}
