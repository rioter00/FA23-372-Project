using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int maxHP, HP;
    public LivingState lState { get; private set; }

    private void Start() {
        HP = maxHP;
        lState = LivingState.ALIVE;
    }

    public void TakeDamage(int damage) {
        HP -= damage;
        if (HP <= 0) {
            Death();
        }
    }

    private void Death() {
        lState = LivingState.DEAD;
        if (gameObject.CompareTag("Enemy")) {
            //call enemy death event
            Destroy(gameObject);
        }
        else {
            //call player death event
        }
    }
}

public enum LivingState {
    ALIVE,
    DEAD,
}
