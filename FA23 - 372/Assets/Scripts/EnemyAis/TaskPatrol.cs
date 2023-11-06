using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System.Security.Cryptography;
using UnityEngine.AI;

public class TaskPatrol : EnemyNode
{
    private List<Transform> waypoints = new List<Transform>();
    private Transform transform;
    //private Animator animator;
    NavMeshAgent Agent;
    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    
    // Start is called before the first frame update
    public TaskPatrol(Transform transform, List<Transform> waypoints, NavMeshAgent Agent) {
        this.waypoints = waypoints;
        this.transform = transform;
        //animator = transform.GetComponent<Animator>();
        
        this.Agent = Agent;
        //for (int i = 0; i < waypoints.Count; i++) { Debug.Log(i); Debug.Log(waypoints[i].position); }
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
            //Debug.Log(wp);
            //if they are at the point then switch to waiting to be true
            if (Vector3.Distance(transform.position, wp.position) < Agent.stoppingDistance)
            {
                //Debug.Log("At Waypoint!!!");
                waitCounter = 0f;
                waiting = true;
                //Debug.Log("hai :3 2");
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                //Debug.Log("hai :3 3");
                //where you tell the animator that you aren't walking
            }
            else {
            //if they aren't waiting and aren't near a point then move them towards the point
                Agent.destination = wp.position;
                transform.LookAt(wp.position);
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
