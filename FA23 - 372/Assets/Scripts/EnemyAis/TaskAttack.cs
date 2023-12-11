using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttack : EnemyNode
{
    private Transform lastTarget;
    private PlayerManager playerManager;
    private float attackTime;
    private float attackCounter = 0f;
    private int damage;
    private Animator animator;
    private Transform enemyTransform;

    public TaskAttack(Transform transform, float attackTime, int damage, Animator animator) { 
        enemyTransform = transform;
        this.attackTime = attackTime;
        this.damage = damage;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        //animator.SetInteger("state", 0);
        
        Transform target = (Transform)GetData("target");
        if (target != lastTarget) { 
            //playerHealth = target.GetComponent<PlayerHealth()>;
            //playerManager = target.GetComponent<PlayerManager>();
            lastTarget = target;
        }

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            //Debug.Log("ATTACKING");
            enemyTransform.LookAt(target);
            animator.SetInteger("state", 2);

            //bool enemyIsDead = playerHealth.TakeDamage(damage);
            //bool enemyIsDead = playerManager.TakeHit(damage);

            /*if (enemyIsDead)
            {
                ClearData("target");
                //switch from atacking to walking in the animator
                animator.SetInteger("state", 1);
            }
            else*/
            //{
            attackCounter = 0f;
            // }
        }
        else { 
            animator.SetInteger("state", 0);
        }
        
        state = NodeState.RUNNING;
        return state;
    }
}
