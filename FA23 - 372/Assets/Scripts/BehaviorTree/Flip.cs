using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Flip : EnemyNode
    {
        public Flip(): base() { }
        public Flip(List<EnemyNode> children) : base(children) { }

        public override NodeState Evaluate() {
            foreach (EnemyNode node in children)
            {
                switch (node.Evaluate()) {
                    case NodeState.RUNNING:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.FAILURE:
                        state = NodeState.SUCCESS;
                        return state;
                    default:
                        return state;
                }
            }
            return state;
        }
    }
}
