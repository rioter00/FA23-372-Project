using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class DrummerEnemyBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;

    public float speed = 2f;
    public float agroRange = 6f;
    public float distanceFromPlayer = .1f;
    public float attackRange = 2f;
    public float attackTime = 1f;
    public bool buffed = false;
    public int attackDamage = 10;


    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node> {
            new Sequence(new List<Node>{
                new CheckInAttackRange(transform,attackRange),
                new TaskAttack(transform, attackTime,attackDamage),
            }),
            /*new Sequence(new List<Node>{
                CheckDrummerAttack();
                new Flip(new List<Node>{
                    CheckBuffed();
             }),
                TaskBuffStats();
             }),
             
             */
            new Sequence(new List<Node>{
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
                new TaskPursue(transform,speed,distanceFromPlayer),
            }),
            new TaskPatrol(transform, waypoints,speed),
        });
        return root;
    }
}
