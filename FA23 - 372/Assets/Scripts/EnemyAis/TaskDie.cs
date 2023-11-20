using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskDie : EnemyNode
{
    GameObject enemy;
    public TaskDie(GameObject enemy) { 
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Die");
        //do animation things
        //i think the object has to be destroyed in the enemy health class
        return NodeState.RUNNING;
        
    }
}
