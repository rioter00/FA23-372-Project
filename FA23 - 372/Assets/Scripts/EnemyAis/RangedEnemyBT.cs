using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

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
                new TaskDie(gameObject),
            }),
            new Sequence(new List<EnemyNode>{
                new CheckInAttackRange(transform, Agent.stoppingDistance),
                new TaskRangedAttack(transform, attackTime,attackDamage, arrowShootPoint, arrowPrefab, arrowSpeed),
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
                new TaskPursue(transform,Agent),
            }),
            new Sequence(new List<EnemyNode>{
                new CheckTimePassed(transform, Agent, gameObject),
                new TaskPatrol(transform, AIOverseer.overseer.waypoints,Agent),
            }),
            new GoToHint(Agent, gameObject),
        });
        return root;
    }
}