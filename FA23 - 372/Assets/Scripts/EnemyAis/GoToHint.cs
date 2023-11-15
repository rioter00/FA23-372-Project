using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GoToHint : EnemyNode
{
    NavMeshAgent Agent;
    Vector3 Hintdestination;
    private float waitTime = 15f;
    private float waitCounter = 0f;
    private bool waiting = false;
    GameObject enemy;
    public GoToHint(NavMeshAgent Agent, GameObject enemy) { 
        this.Agent = Agent;
        Hintdestination = Agent.destination;
        this.enemy = enemy;

    }

    public override NodeState Evaluate()
    {
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
        else
        {
            if (Vector3.Distance(enemy.transform.position, Agent.destination) < Agent.stoppingDistance)
            {
                //Debug.Log(Agent.destination);
                //.destination = Hintdestination;
                enemy.GetComponent<VisitedHint>().visitedHint = true;
                waitCounter = 0f;
                waiting = true;
            }
        }
        //Debug.Log(enemy.GetComponent<VisitedHint>().visitedHint);
        state = NodeState.RUNNING;
        return state;
    }
}
