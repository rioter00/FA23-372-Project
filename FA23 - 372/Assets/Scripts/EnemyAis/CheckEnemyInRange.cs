using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInRange : Node
{
    private Transform transform;
    public static int playerLayerMask = 1 << 6;
    private float agroRange;
    //private AIOverseer AIO;
    private GameObject Enemy;

    public CheckEnemyInRange(GameObject Enemy, float agroRange)// AIOverseer AIO
    {
        transform = Enemy.transform;
        this.Enemy = Enemy;
        this.agroRange = agroRange;
        //this.AIO = AIO;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target == null)
        {
            //get a list of the player colliders that are in the agro range of the enemy
            Collider[] initialColliders = Physics.OverlapSphere(transform.position, agroRange, playerLayerMask);

            if (initialColliders.Length > 0)
            {
                //uses parent.parent because the parent is 2 nodes about this and we want to set this in the dictionary of the node so the whole tree can access it
                parent.parent.SetData("target", initialColliders[0].transform);

                //AIO.ReportFightingAgentAddition(Enemy);

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
