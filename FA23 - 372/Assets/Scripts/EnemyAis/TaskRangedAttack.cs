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
    private FireProjectile fp;


    public TaskRangedAttack(Transform transform, float attackTime, int damage, Transform asp, GameObject arrowPrefab, float arrowSpeed)
    {
        this.attackTime = attackTime;
        this.damage = damage;
        arrowShootPoint = asp;
        this.arrowPrefab = arrowPrefab;
        this.arrowSpeed = arrowSpeed;
        fp = new FireProjectile();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != lastTarget)
        {
            playerManager = target.GetComponent<PlayerManager>();
            lastTarget = target;
        }

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            //bool enemyIsDead = playerManager.TakeHit(damage);
            fp.shoot(arrowPrefab, arrowShootPoint, arrowSpeed);
            /*if (enemyIsDead)
            {
                ClearData("target");
                //switch from atacking to walking in the animator
            }
            else
            {*/
            attackCounter = 0f;
            //}
        }

        state = NodeState.RUNNING;
        return state;
    }

}
