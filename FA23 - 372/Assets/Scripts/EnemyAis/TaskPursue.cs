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
    private Animator animator;

    public TaskPursue(Transform transfom, NavMeshAgent Agent, Animator animator)
    {
        this.transform = transfom;
        this.Agent = Agent;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        animator.SetInteger("state", 1);
        Transform target = (Transform)GetData("target");
        Agent.destination= target.position;
        state = NodeState.RUNNING; 
        return state;
    }
}
