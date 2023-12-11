using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEditor.Experimental.GraphView;

public class TaskRangedAttack : EnemyNode
{
    private Transform lastTarget;
    private PlayerManager playerManager;
    private float attackTime;
    private float attackCounter = 0f;
    private int damage;
    private Transform arrowShootPoint;
    private GameObject arrowPrefab;
    private float arrowSpeed;
    private Animator animator;
    private Transform enemyTransform;


    public TaskRangedAttack(Transform transform, float attackTime, int damage, Transform asp, GameObject arrowPrefab, float arrowSpeed, Animator animator)
    {
        enemyTransform = transform;
        this.attackTime = attackTime;
        this.damage = damage;
        arrowShootPoint = asp;
        this.arrowPrefab = arrowPrefab;
        this.arrowSpeed = arrowSpeed;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != lastTarget)
        {
            //playerHealth = target.GetComponent<PlayerHealth()>;
            playerManager = target.GetComponent<PlayerManager>();
            lastTarget = target;
        }

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            enemyTransform.LookAt(target);
            animator.SetInteger("state", 2);

            //bool enemyIsDead = playerHealth.TakeDamage(damage);
            //bool enemyIsDead = playerManager.TakeHit(damage);
            FireProjectile.shoot(arrowPrefab, arrowShootPoint, arrowSpeed);
            /*if (enemyIsDead)
            {
                ClearData("target");
                animator.SetInteger("state", 1);
                //switch from atacking to walking in the animator
            }*/
            //else
            //{
            attackCounter = 0f;
            //}
        }
        else { 
            animator.SetInteger("state", 0);
        }

        state = NodeState.RUNNING;
        return state;
    }

}
