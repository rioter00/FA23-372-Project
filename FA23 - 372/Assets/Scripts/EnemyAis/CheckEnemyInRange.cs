using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.UIElements;

public class CheckEnemyInRange : EnemyNode
{
    private Transform Enemytransform;
    public static int playerLayerMask = 1 << 6;
    private float agroRange;
    float angle;
    float seeAngle;

    public CheckEnemyInRange(Transform transform, float agroRange)
    {
        this.Enemytransform = transform;
        this.agroRange = agroRange;
        //this.seeAngle = seeAngle;
       // Debug.Log(this.seeAngle);
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target == null)
        {
            
            //get a list of the player colliders that are in the agro range of the enemy
            Collider[] initialColliders = Physics.OverlapSphere(Enemytransform.position, agroRange, playerLayerMask);
            if (initialColliders.Length > 0)
            {
                Vector3 playerPos = (initialColliders[0].transform.position).normalized;
                Vector3 direction = (playerPos-Enemytransform.position).normalized;
                angle = Vector3.Angle(direction, Enemytransform.forward);
                //Debug.Log(angle);
            }

            //if (initialColliders.Length > 0)
            if(initialColliders.Length > 0 && angle < 100)// 
            {
                
                AIOverseer.overseer.ReportFightingAgentAddition(Enemytransform.gameObject);

                //uses parent.parent because the parent is 2 nodes about this and we want to set this in the dictionary of the node so the whole tree can access it
                parent.parent.SetData("target", initialColliders[0].transform);
                //set animation
                state = NodeState.SUCCESS;
                return state;
            }
            else {
                ClearData("target");
            }

            state = NodeState.FAILURE;
            return state;
        }

       

        state = NodeState.SUCCESS;
        return state;
    }
}
