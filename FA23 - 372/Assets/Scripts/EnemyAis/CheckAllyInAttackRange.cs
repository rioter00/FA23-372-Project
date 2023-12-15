using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckAllyInAttackRange : EnemyNode
{
    private Transform transform;
    //instantiate animator
    private float attackRange;
    private Animator animator;

    public CheckAllyInAttackRange(Transform transform, float attackRange)//, Animator animator
    {
        this.transform = transform;
        this.attackRange = attackRange;
        //this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        Transform target = (Transform)t;
        if (AIOverseer.overseer.activeAgents.Count != 0 && Vector3.Distance(transform.position, target.position) <= attackRange) {
            state = NodeState.SUCCESS;
            return state;
        }

        /*Transform target = (Transform)t;
        //checks is the distance between the enemy and the player is smaller than the attack range
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            //set the walking animation to be false and the attack animation to be true
            //animator.SetInteger("state", 2);

            state = NodeState.SUCCESS;
            return state;
        }*/

        state = NodeState.FAILURE;
        return state;
    }
}