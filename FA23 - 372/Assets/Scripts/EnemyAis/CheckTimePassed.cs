using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
using Unity.VisualScripting;

public class CheckTimePassed : EnemyNode
{
    //float time = 0f;
    float stopTime = 10f;
    //bool wentToHint = false;
    Transform transform;
    NavMeshAgent agent;
    bool accomplished;
    GameObject enemy;

    public CheckTimePassed(Transform transform, NavMeshAgent Agent, GameObject enemy) {
        this.transform = transform;
        this.agent = Agent;
        
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log(accomplished);
        
        if (enemy.GetComponent<VisitedHint>().visitedHint == true)
        {
            state = NodeState.SUCCESS; 
            return state;
        }

        state = NodeState.FAILURE;
        return state;
        
    }
}
