using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System;
using UnityEngine.AI;

public class TaskBuglerAttack : EnemyNode
{
    private Transform transform;
    private PlayerManager playerManager;
    private float attackTime;
    private float attackCounter = 0f;
    private int damage;
    private Animator animator;

    public TaskBuglerAttack(Transform transform, float attackTime, int damage, Animator animator)//
    {
        this.attackTime = attackTime;
        this.damage = damage;
        this.transform = transform;
        this.animator = animator;
    }

    public override NodeState Evaluate()
    {
        //animator.SetInteger("state", 0);
        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            animator.SetInteger("state", 2);
            foreach (GameObject Ally in AIOverseer.overseer.activeAgents)
            {
                float distance = Vector3.Distance(Ally.transform.position, transform.position);
                if (distance < 8)
                {
                    if (Ally.GetComponent<Buffed>().isBuffed == true) { }
                    else
                    {
                        Ally.GetComponent<Buffed>().isBuffed = true;
                        Ally.GetComponent<EnemyTree>().attackTime -= 0.5f;
                        Ally.GetComponent<NavMeshAgent>().speed += 5;
                    }
                }
            }
            attackCounter = 0f;
        }

        /*Transform target = (Transform)GetData("target");
        if (target != lastTarget)
        {
            playerManager = target.GetComponent<PlayerManager>();
            lastTarget = target;
        }

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime)
        {
            Debug.Log("ATTACKING");
            

            bool enemyIsDead = playerManager.TakeHit(damage);

            if (enemyIsDead)
            {
                ClearData("target");
                //switch from atacking to walking in the animator
                animator.SetInteger("state", 1);
            }
            else
            {
                attackCounter = 0f;
            }
        }*/

        state = NodeState.RUNNING;
        return state;
    }
}
