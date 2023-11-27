using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class DrummerEnemyBT : BehaviorTree.EnemyTree
{
    // nick changed this from array to list
    public List<Transform> waypoints;

    public float speed = 2f;
    public float agroRange = 6f;
    public float distanceFromPlayer = .1f;
    public float attackRange = 2f;
    public float attackTime = 1f;
    public bool buffed = false;
    public int attackDamage = 10;


    protected override EnemyNode SetupTree()
    {
        EnemyNode root = new Selector(new List<EnemyNode> {
            new Sequence(new List<EnemyNode>{
                // fix this line
                new CheckInAttackRange(transform, attackRange, null),
                // fix this line
                new TaskAttack(transform, attackTime,attackDamage, null),
            }),
            /*new Sequence(new List<Node>{
                CheckDrummerAttack();
                new Flip(new List<Node>{
                    CheckBuffed();
             }),
                TaskBuffStats();
             }),
             
             */
            new Sequence(new List<EnemyNode>{
                new CheckTeammateInRange(transform,agroRange),
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
               // fix this line
               // new TaskPursue(transform,speed,distanceFromPlayer, null),
               // new TaskPursue(transform,speed,distanceFromPlayer, null),
            }),
            // fix this line
            new TaskPatrol(transform, waypoints, null , null),
        });
        return root;
    }
}
