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
    private Animator animator;
    NavMeshAgent Agent;
    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;
    private float shortestDistance = 10000f;
    private int shortestWaypointIndex;
    private bool patroling = false;

    
    public TaskPatrol(Transform transform, List<Transform> waypoints, NavMeshAgent Agent, Animator animator) {
        this.waypoints = waypoints;
        this.transform = transform;
        this.animator = animator;
        this.Agent = Agent;
    }

    public override NodeState Evaluate()
    {
        if (!patroling) {
            int i = 0;
            foreach (Transform t in waypoints)
            {
                float distance = Vector3.Distance(t.position, transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestWaypointIndex = i;
                }
                i++;

            }
            currentWaypointIndex = shortestWaypointIndex;
        }
        patroling = true;
        
        
        //check if enemy is waiting at a point
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            //if they have waited long enough then they are walking
            if (waitCounter >= waitTime)
            {
                waiting = false;
                //this is where you set the animator to walking
                animator.SetInteger("state", 1);

            }
        }
        else {
            
            Transform wp = waypoints[currentWaypointIndex];

            Debug.Log(wp);
            //if they are at the point then switch to waiting to be true
            if (Vector3.Distance(transform.position, wp.position) < Agent.stoppingDistance)
            {
                //Debug.Log("At Waypoint!!!");
                waitCounter = 0f;
                waiting = true;
                //Debug.Log("hai :3 2");
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                //Debug.Log(currentWaypointIndex);
                //where you tell the animator that you aren't walking
                animator.SetInteger("state", 0);
            }
            else {
                //if they aren't waiting and aren't near a point then move them towards the point
                animator.SetInteger("state", 1);
                Agent.destination = wp.position;
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
