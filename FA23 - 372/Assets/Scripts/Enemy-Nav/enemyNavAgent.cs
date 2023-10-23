using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyNavAgent : MonoBehaviour
{
    [SerializeField] private Transform movePostionTransform;
    private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        navMeshAgent.destination = movePostionTransform.position;
    }
}
