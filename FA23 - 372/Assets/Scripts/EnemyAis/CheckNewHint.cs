using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class CheckNewHint : EnemyNode
{
    private Transform Enemytransform;
    NavMeshAgent agent;
    GameObject enemy;

    public CheckNewHint(Transform transform, NavMeshAgent agent, GameObject enemy)
    {
        this.Enemytransform = transform;
        this.agent = agent;
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        object hint = GetData("Hint");
        if (hint == null)
        {
            parent.parent.SetData("Hint", agent.destination);
        }
        else {
            if ((Vector3)hint == agent.destination)
            {
                state = NodeState.SUCCESS;
                return state;
            }
            else {
                Debug.Log("Hints are dif");
                ClearData("Hint");
                enemy.GetComponent<VisitedHint>().visitedHint = false;
                state = NodeState.FAILURE;
                return state;
            }
        }
        state = NodeState.FAILURE;
        return state;

    }
}
