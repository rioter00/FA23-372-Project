using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RangedEnemyBT : BehaviorTree.EnemyTree
{
    public UnityEngine.Transform[] waypoints;

    [SerializeField] float speed = 2f;
    //public float agroRange = 6f;

    [SerializeField] float distanceFromPlayer = .1f;
    // public float attackRange = 2f;
    //public float attackTime = 1f;
    [SerializeField] int attackDamage = 10;
    [SerializeField] Transform arrowShootPoint;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float arrowSpeed;


    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node> {
         new Sequence(new List<Node>{
             new CheckInAttackRange(transform,attackRange),
             new TaskRangedAttack(transform, attackTime, attackDamage, arrowShootPoint, arrowPrefab, arrowSpeed),
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
             new CheckEnemyInRange(gameObject,agroRange),
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