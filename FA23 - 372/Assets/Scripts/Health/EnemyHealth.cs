using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public LivingState LState { get; set; }
    Animator animator;

    public void TakeDamage(int damage){
        HP -= damage;
        if (HP <= 0){
            Death();
        }
    }
    public void Death(){
        LState = LivingState.DEAD;
        StartCoroutine(DeathWait());
    }

    private void Start(){
        MaxHP = 1;
        HP = MaxHP;
        LState = LivingState.ALIVE;
        animator = gameObject.GetComponent<Animator>();
    }

    IEnumerator DeathWait() {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
