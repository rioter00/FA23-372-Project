using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree 
{
    public enum NodeState { 
        RUNNING, SUCCESS, FAILURE
    }

    public class EnemyNode
    {
        protected NodeState state;

        public EnemyNode parent;
        protected List<EnemyNode> children = new List<EnemyNode>();

        private Dictionary<string, object> dataContext = new Dictionary<string, object>();

        //constructors
        public EnemyNode() {
            parent = null;
        }

        public EnemyNode(List<EnemyNode> children) {
            foreach (EnemyNode child in children) {
                Attach(child);
            }
        }

        //to attach a node to the tree
        private void Attach(EnemyNode node) {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        //to share data between nodes
        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object val = null;
            if (dataContext.TryGetValue(key, out val)) {
                return val;
            }

            EnemyNode node = parent;
            if (node != null)
            {
                val = node.GetData(key);
            }
            return val;
        }

        public bool ClearData(string key) {
            bool cleared = false;
            if (dataContext.ContainsKey(key)) { 
                dataContext.Remove(key);
                return true;
            }

            EnemyNode node = parent;
            if (node != null)
            {
                cleared = node.ClearData(key);
            }
            return true;
        }
    }

}
