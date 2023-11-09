using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System.Security.Cryptography;

public class TaskPatrol : EnemyNode
{
    private Transform[] waypoints;
    private Transform transform;
    //private Animator animator;

    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    private float speed;
    // Start is called before the first frame update
    public TaskPatrol(Transform transform, Transform[] waypoints, float speed) {
        this.waypoints = waypoints;
        this.transform = transform;
        //animator = transform.GetComponent<Animator>();
        this.speed = speed;
    }

    public override NodeState Evaluate()
    {
        //check if enemy is waiting at a point
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            //if they have waited long enough then they are walking
            if (waitCounter >= waitTime)
            {
                waiting = false;
                //this is where you set the animator to walking
            }
        }
        else {
            Transform wp = waypoints[currentWaypointIndex];
            //if they are at the point then switch to waiting to be true
            if (Vector3.Distance(transform.position, wp.position) < 0.01f)
            {
                transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                //where you tell the animator that you aren't walking
            }
            else {
            //if they aren't waiting and aren't near a point then move them towards the point
                transform.position = Vector3.MoveTowards(transform.position, wp.position, speed * Time.deltaTime);
                transform.LookAt(wp.position);
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
