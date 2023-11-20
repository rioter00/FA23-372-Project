using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        //constructors
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate() {
            bool anyChildIsRunning = false;

            //checks all children to see if they are all successful
            foreach (Node node in children) {
                switch (node.Evaluate()) {
                    //if one child is failing return failure
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    //if one child is successful then continue on to the next child
                    case NodeState.SUCCESS:
                        continue;
                    //if one child is running then continue to the next one
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;

                }
            }
            //checks if the child is running or not. if it is then return running if not then return success
            //those are the only two options at this point because we would have returned failure eariler
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}