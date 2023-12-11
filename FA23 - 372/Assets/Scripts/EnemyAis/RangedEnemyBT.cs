using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class RangedEnemyBT : BehaviorTree.EnemyTree
{
    private float time;
    [SerializeField]
    private Transform arrowShootPoint;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private float arrowSpeed;
    protected override EnemyNode SetupTree()
    {

        EnemyNode root = new Selector(new List<EnemyNode> {
            new Sequence(new List<EnemyNode>{
                new CheckHealth(gameObject),
                new TaskDie(gameObject, gameObject.GetComponent<Animator>(), gameObject.GetComponent<NavMeshAgent>()),
            }),
            new Sequence(new List<EnemyNode>{
                new CheckInAttackRange(transform, Agent.stoppingDistance-0.5f, gameObject.GetComponent<Animator>()),
                new TaskRangedAttack(transform, attackTime,attackDamage, arrowShootPoint, arrowPrefab, arrowSpeed, gameObject.GetComponent<Animator>()),
            }),
            new Sequence(new List<EnemyNode>{
                new CheckEnemyInRange(transform,agroRange),
               /* new Sequence(new List<Node>{ 
                    new Flip(new List<Node>{
                        CheckStunned();
                }),
                  new Selector(new List<Node>{
                    TaskRushPursue(),
                    TaskDodgePursue(),
                    TaskPursue(),
                }),
                }),*/
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