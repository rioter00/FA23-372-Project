using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth{
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public LivingState LState { get; set; }

    public void TakeDamage(int damage){
        HP -= damage;
        if (HP <= 0){
            Death();
        }
    }
    public void Death(){
        LState = LivingState.DEAD;
        //Do other death things with player
    }

    private void Start(){
        HP = MaxHP;
        LState = LivingState.ALIVE;
    }
}
