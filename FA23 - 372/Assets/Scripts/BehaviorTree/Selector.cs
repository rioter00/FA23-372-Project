using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : EnemyNode
    {
        public Selector() : base() { }
        public Selector(List<EnemyNode> children) : base(children) { }

        public override NodeState Evaluate() { 
            //go through all of the children
            foreach (EnemyNode node in children)
            {
                switch (node.Evaluate()) {
                    //if the node fails then continue to the next node
                    case NodeState.FAILURE:
                        continue;
                    //if the node is successful then exit and succeed
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    //if the node is running then exit and state running
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }

    }
}
