using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEditor;

public class TaskPursue : Node
{
    private Transform transform;
    private float speed;
    private float distanceFromPlayer;
    //private float speed = MeleeEnemyBT.speed;

    public TaskPursue(Transform transfom, float speed, float distanceFromPlayer)
    {
        this.transform = transfom;
        this.speed = speed;
        this.distanceFromPlayer = distanceFromPlayer;

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(transform.position, target.position) > distanceFromPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(transform.position);
        }
        

        state = NodeState.RUNNING; 
        return state;
    }
}
