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
                new TaskDie(gameObject),
            }),
            new Sequence(new List<EnemyNode>{
                new CheckInAttackRange(transform, Agent.stoppingDistance),
                new TaskAttack(transform, attackTime,attackDamage),
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
