using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GoToHint : EnemyNode
{
    NavMeshAgent Agent;
    public GoToHint(NavMeshAgent Agent) { 
    this.Agent = Agent;

    }

    public override NodeState Evaluate()
    {
        //Debug.Log(Agent.destination);
        Agent.destination = Agent.destination;
        state = NodeState.RUNNING;
        return state;
    }
}
