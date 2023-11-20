using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree { 
    public abstract class EnemyTree : MonoBehaviour
    {
        private EnemyNode root = null;
        public NavMeshAgent Agent;
        public float AttackRange;
        public float attackTime = 1f;
        public int attackDamage = 10;
        public float agroRange = 6f;
        //public float seeAngle = 100f;

        protected void Start()
        {
            root = SetupTree();
            Agent = GetComponent<NavMeshAgent>();
            AttackRange = Agent.stoppingDistance;
            
        }

        private void Update()
        {
            if (root != null) {
                root.Evaluate();
            }
        }

        protected abstract EnemyNode SetupTree();
    }
}