using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEditor;
using UnityEngine.AI;

public class TaskPursue : EnemyNode
{
    private Transform transform;
    private NavMeshAgent Agent;

    public TaskPursue(Transform transfom, NavMeshAgent Agent)
    {
        this.transform = transfom;
        this.Agent = Agent;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        Agent.destination= target.position;
        state = NodeState.RUNNING; 
        return state;
    }
}
