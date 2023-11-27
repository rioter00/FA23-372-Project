using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHealth : EnemyNode
{
    private EnemyHealth enemyHealth;
    public CheckHealth(GameObject enemy) {
        enemyHealth = enemy.GetComponent<EnemyHealth>();
    }

    public override NodeState Evaluate()
    {
        if (enemyHealth.LState == LivingState.DEAD)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        else {
            return NodeState.FAILURE;
        }
    }
}
