using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class BuglerEnemyBT : BehaviorTree.EnemyTree
{
    private float time;
    protected override EnemyNode SetupTree()
    {

        EnemyNode root = new Selector(new List<EnemyNode> {
           new Sequence(new List<EnemyNode>{
                new CheckHealth(gameObject),
                new TaskDie(gameObject, gameObject.GetComponent < Animator >(), gameObject.GetComponent<NavMeshAgent>()),
            }),
            new Sequence(new List<EnemyNode>{
                new CheckAllyInAttackRange(transform, Agent.stoppingDistance),// gameObject.GetComponent<Animator>()
                new TaskBuglerAttack(transform, attackTime,attackDamage, gameObject.GetComponent<Animator>()),//
            }),
            new Sequence(new List<EnemyNode>{
                new CheckEnemyInRange(transform,agroRange),
                new TaskPursue(transform,Agent, gameObject.GetComponent < Animator >()),
            }),
            new Sequence(new List<EnemyNode>{
                new CheckTimePassed(transform, Agent, gameObject),
                new TaskPatrol(transform, AIOverseer.overseer.waypoints,Agent, gameObject.GetComponent<Animator>()),
            }),
            new GoToHint(Agent, gameObject, gameObject.GetComponent<Animator>()),
        });
        return root;
    }
}
