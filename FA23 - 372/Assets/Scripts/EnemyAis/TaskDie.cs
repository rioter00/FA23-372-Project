using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
public class TaskDie : EnemyNode
{
    GameObject enemy;
    Animator animator;
    NavMeshAgent agent;

    public TaskDie(GameObject enemy, Animator animator, NavMeshAgent agent) { 
        this.enemy = enemy;
        this.animator = animator;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Die");
        //do animation things
        animator.SetInteger("state", 3);
        agent.SetDestination(enemy.transform.position);
        //i think the object has to be destroyed in the enemy health class
        return NodeState.RUNNING;
        
    }

}
