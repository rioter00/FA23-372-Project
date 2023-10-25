using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttack : Node
{
    private Transform lastTarget;
    private PlayerManager playerManager;
    private float attackTime;
    private float attackCounter = 0f;
    private int damage;

    public TaskAttack(Transform transform, float attackTime, int damage) { 
        this.attackTime = attackTime;
        this.damage = damage;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != lastTarget) { 
            playerManager = target.GetComponent<PlayerManager>();
            lastTarget = target;
        }

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime) {
            bool enemyIsDead = playerManager.TakeHit(damage);

            if (enemyIsDead)
            {
                ClearData("target");
                //switch from atacking to walking in the animator
            }
            else
            {
                attackCounter = 0f;
            }
        }
        
        state = NodeState.RUNNING;
        return state;
    }
}
