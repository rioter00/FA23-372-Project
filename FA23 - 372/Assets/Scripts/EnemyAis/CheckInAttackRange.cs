using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckInAttackRange : Node
{
    private Transform transform;
    //instantiate animator
    private float attackRange;

    public CheckInAttackRange(Transform transform,float attackRange) { 
        this.transform = transform;
        this.attackRange = attackRange;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null) {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        //checks is the distance between the enemy and the player is smaller than the attack range
        if (Vector3.Distance(transform.position, target.position) <= attackRange) { 
            //set the walking animation to be false and the attack animation to be true

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
