using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIOverseer : MonoBehaviour
{
    public List<GameObject> activeAgents = new List<GameObject>();
    private List<GameObject> fightingAgents = new List<GameObject>();
    public static AIOverseer overseer = null;

    private Transform playerTransform;
    private Vector3 hintLocation;
    [SerializeField] private float hintVariationRange = 5f;
    [Range(0,1)]
    [SerializeField] private float alertMin = 0f;
    [Range(0, 1)]
    [SerializeField] private float alertMax = 1f;
    [Range(0, 1)]
    [SerializeField] private float alertThreshold = 0.5f;

    public List<Transform> waypoints = new List<Transform>();


    private void Awake()
    {
        if (overseer == null)
        {
            overseer = this;
        }
        else Destroy(this);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");
        for (int i = 0; i < waypointObjects.Length; i++) waypoints.Add(waypointObjects[i].transform);
    }

    private void Start()
    {
        if (GameManager.enabledGameManager.silenceManagers) return;
        hintLocation = GenerateNewHintLocation();
    }

    public void ReportAgentAddition(GameObject agentToAdd) {
        if (GameManager.enabledGameManager.silenceManagers) return;
        activeAgents.Add(agentToAdd); 
    }
    public void ReportAgentSubtraction(GameObject agentToSubtract) {
        if (GameManager.enabledGameManager.silenceManagers) return;
        activeAgents.Remove(agentToSubtract);
        if (activeAgents.Count < 1) SignalWaveSpawn();
    }
    public void ReportFightingAgentAddition(GameObject agentToAdd) {
        if (GameManager.enabledGameManager.silenceManagers) return;
        fightingAgents.Add(agentToAdd); 
    }
    public void ReportFightingAgentSubtraction(GameObject agentToSubtract) {
        if (GameManager.enabledGameManager.silenceManagers) return;
        fightingAgents.Remove(agentToSubtract); 
    }

    private Vector3 GenerateNewHintLocation()
    {
        NavMeshHit hit;
        Vector3 hint = playerTransform.position + new Vector3(Random.Range(hintVariationRange, -hintVariationRange), playerTransform.position.y, Random.Range(hintVariationRange, -hintVariationRange));
        NavMesh.SamplePosition(hint, out hit, 10f, NavMesh.AllAreas);
        return hit.position;
    }

    public void GiveHintToAgent(NavMeshAgent agent)
    {
        if (GameManager.enabledGameManager.silenceManagers) return;
        hintLocation = GenerateNewHintLocation();
        agent.destination = hintLocation;
    }

    private void GiveHintToAllAgents()
    {
        hintLocation = GenerateNewHintLocation();
        foreach(GameObject g in activeAgents)
        {
            g.GetComponent<NavMeshAgent>().destination = hintLocation;
            //g.GetComponent<VisitedHint>().visitedHint = false;
        }
    }

    private void SignalWaveSpawn()
    {
        hintLocation = GenerateNewHintLocation();
        SpawnManager.enabledSpawnManager.SpawnWave();
    }

   /*+ public void SignalDodgeToFightingAgents()
    {
        if (GameManager.enabledGameManager.silenceManagers) return;
        foreach (GameObject g in fightingAgents) if(Random.Range(alertMin, alertMax) > alertThreshold) { g.GetComponent<IAgent>().SetBehaviorState(0); }   //placeholder parameter    
    }

    public void SignalRushToFightingAgents()
    {
        if (GameManager.enabledGameManager.silenceManagers) return;
        foreach (GameObject g in fightingAgents) if(Random.Range(alertMin, alertMax) > alertThreshold) { g.GetComponent<IAgent>().SetBehaviorState(1); }   //placeholder parameter
    }*/

}
