using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree { 
    public abstract class EnemyTree : MonoBehaviour
    {
        private EnemyNode root = null;

        protected void Start()
        {
            root = SetupTree();
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