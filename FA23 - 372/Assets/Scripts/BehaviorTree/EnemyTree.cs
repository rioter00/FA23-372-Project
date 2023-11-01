using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class EnemyTree : MonoBehaviour//, IAgent
    {
        private Node root = null;
        public bool rush = false;
        public bool dodge = false;
        public float agroRange = 6f;
        public float attackRange = 2f;
        public float attackTime = 1f;
        //public AIOverseer AIO;
        //private GameObject overseerManager;

        protected void Start()
        {
            root = SetupTree();
            //AIO = overseerManager.GetComponent<AIOverseer>();
        }

        private void Update()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }

        protected abstract Node SetupTree();

        public void SetBehaviorState(int behavior)
        {
            if (behavior == 0)
            {
                dodge = true;
                rush = false;
            }
            else if (behavior == 1)
            {
                rush = true;
                dodge = false;
            }
            else
            {
                rush = false;
                dodge = false;
            }
        }

    }
}