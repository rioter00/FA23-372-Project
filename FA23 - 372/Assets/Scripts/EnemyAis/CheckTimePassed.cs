using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class CheckTimePassed : EnemyNode
{
    //float time = 0f;
    float stopTime = 10f;
    //bool wentToHint = false;
    Transform transform;
    NavMeshAgent agent;

    public CheckTimePassed(Transform transform, NavMeshAgent Agent) {
        this.transform = transform;
        this.agent = Agent;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log(wentToHint);
        if (Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance)
        {
            state = NodeState.FAILURE;
            return state;
        }

        stopTime -= Time.deltaTime;
        if (stopTime <= 0.0f) { 
            state = NodeState.SUCCESS;
            return state;
        }
        /*while (stopTime >= 0.0f) {
            Debug.Log("Reached Dest");
            
            wentToHint = true;
        }
        /*else if (stopTime >= 0.0f)
        {
            Debug.Log("Reached Dest");
            stopTime -= Time.deltaTime;
            wentToHint = true;
            state = NodeState.FAILURE;
            return state;
        }*/
  
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }
}
