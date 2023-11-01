using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
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
        Destroy(gameObject); //for testing
        //Do things with enemy death
    }

    private void Start(){
        HP = MaxHP;
        LState = LivingState.ALIVE;
    }


}
