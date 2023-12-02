using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GoToHint : EnemyNode
{
    NavMeshAgent Agent;
    Vector3 Hintdestination;
    private float waitTime = 5f;
    private float waitCounter = 0f;
    private bool waiting = false;
    private GameObject enemy;
    private Animator animator;
    public GoToHint(NavMeshAgent Agent, GameObject enemy, Animator animator) { 
        this.Agent = Agent;
        Hintdestination = Agent.destination;
        this.enemy = enemy;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        animator.SetInteger("state", 1);
        if (waiting)
        {
            animator.SetInteger("state", 0);
            waitCounter += Time.deltaTime;
            //if they have waited long enough then they are walking
            if (waitCounter >= waitTime)
            {
                waiting = false;
                enemy.GetComponent<VisitedHint>().visitedHint = true;
                //this is where you set the animator to walking
                //animator.SetInteger("state", 0);
            }
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, Agent.destination) < Agent.stoppingDistance)
            {
                //Debug.Log(Agent.destination);
                //.destination = Hintdestination;
                
                waitCounter = 0f;
                waiting = true;
            }
        }
        //Debug.Log(enemy.GetComponent<VisitedHint>().visitedHint);
        state = NodeState.RUNNING;
        return state;
    }
}
