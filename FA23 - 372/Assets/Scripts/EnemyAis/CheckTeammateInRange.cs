using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckTeammateInRange : Node
{
    private Transform transform;
    public static int enemyLayerMask = 1 << 7;
    private float agroRange;

    public CheckTeammateInRange(Transform transform, float agroRange)
    {
        this.transform = transform;
        this.agroRange = agroRange;

    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target == null)
        {
            //get a list of the player colliders that are in the agro range of the enemy
            Collider[] initialColliders = Physics.OverlapSphere(transform.position, agroRange, enemyLayerMask);
            Debug.Log(initialColliders.Length);
            if (initialColliders.Length > 0)
            {
                //uses parent.parent because the parent is 2 nodes about this and we want to set this in the dictionary of the node so the whole tree can access it
                parent.parent.SetData("target", initialColliders[0].transform);
                //set animation
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                ClearData("target");
            }

            state = NodeState.FAILURE;
            return state;
        }



        state = NodeState.SUCCESS;
        return state;
    }
}
