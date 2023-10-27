using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager enabledSpawnManager = null;

    private List<SpawnLocation> openSpawnLocations = new List<SpawnLocation>();

    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject bowmenPrefab;
    [SerializeField] private GameObject buglerPrefab;

    void Awake()
    {
        if (enabledSpawnManager == null) enabledSpawnManager = this;
        else Destroy(this);
    }

    public void AddOpenSpawnLocation(SpawnLocation newLocation)
    {
        openSpawnLocations.Add(newLocation);
    }

    private void RemoveOpenSpawnLocation(SpawnLocation location)
    {
        openSpawnLocations.Remove(location);
    }

    public void SpawnWave()
    {
        int wave = GameManager.enabledGameManager.waveCount;
        int quota;
        if (wave == 0) quota = 3;
        else quota = wave * 2; //placeholder calculation, we can make this more complex later
        while (quota > 0)
        {
            SpawnLocation chosenLocation = openSpawnLocations[Random.Range(0, openSpawnLocations.Count)];
            for(int i = 0; i < chosenLocation.prefabCap; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(chosenLocation.spawnBoundsMin.x, chosenLocation.spawnBoundsMax.x),
                                               chosenLocation.transform.position.y,
                                               Random.Range(chosenLocation.spawnBoundsMin.y, chosenLocation.spawnBoundsMax.y));
                GameObject nextSoldier = Instantiate(knightPrefab, spawnPos, Quaternion.identity);
                AIOverseer.overseer.ReportAgentAddition(nextSoldier);
                AIOverseer.overseer.GiveHintToAgent(nextSoldier.GetComponent<NavMeshAgent>());
                quota--;
                if (quota > 0) break;
            }
            if (chosenLocation.neighbors.Length > 0)
            {
                foreach (SpawnLocation s in chosenLocation.neighbors)
                {
                    if (!s.open)
                    {
                        s.open = true;
                        AddOpenSpawnLocation(s);
                    }
                }
                chosenLocation.open = false;
                RemoveOpenSpawnLocation(chosenLocation);
            }
            chosenLocation = null;
        }
        GameManager.enabledGameManager.waveCount++;
    }
}
