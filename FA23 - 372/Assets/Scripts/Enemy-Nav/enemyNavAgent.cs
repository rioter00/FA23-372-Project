using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyNavAgent : MonoBehaviour
{
    [SerializeField] private Transform movePostionTransform;
    private NavMeshAgent navMeshAgent;
    [SerializeField] List<Transform> wayPoint;
    public int currentWayPointIndex = 0;
    public bool pursue = false;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(movePostionTransform.position, this.gameObject.transform.position);
        if (distanceToPlayer <= 3)
        {
            pursue = true;
        }
        if (pursue)
        {
            navMeshAgent.destination = movePostionTransform.position;
        }
        else
        {
            patrol();
        }
    }
    void patrol()
    {
        int random = Random.Range(0, wayPoint.Count);
        if (wayPoint.Count == 0)
        {
            return;
        }

        float distanceToWayPoint = Vector3.Distance(wayPoint[currentWayPointIndex].position, this.gameObject.transform.position);
        if(distanceToWayPoint <= 5)
        {
            currentWayPointIndex = (random) % wayPoint.Count;
        }
        navMeshAgent.SetDestination(wayPoint[currentWayPointIndex].position);
    }
}
