using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskDie : EnemyNode
{
    GameObject enemy;
    Animator animator;
    public TaskDie(GameObject enemy, Animator animator) { 
        this.enemy = enemy;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Die");
        //do animation things
        animator.SetInteger("state", 3);
        //i think the object has to be destroyed in the enemy health class
        return NodeState.RUNNING;
        
    }

}
