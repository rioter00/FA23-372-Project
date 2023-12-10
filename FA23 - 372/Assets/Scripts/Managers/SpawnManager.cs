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

    [SerializeField] private bool UseNewQuotaSystem = false;

    [SerializeField] private int[] knightWaveQuotas;
    [SerializeField] private int[] bowmenWaveQuotas;
    [SerializeField] private int[] buglerWaveQuotas;

    private List<List<int>> quotaMatrix = new List<List<int>>();
    private int smallestQuota;

    void Awake()
    {
        if (enabledSpawnManager == null) enabledSpawnManager = this;
        else Destroy(this);

        for(int i = 0; i < 3; i++)
        {
            quotaMatrix.Add(new List<int>());
        }


        for (int i = 0; i < knightWaveQuotas.Length; i++)
        {
            
            if (knightWaveQuotas[i] < 0) quotaMatrix[0].Add(0);
            else quotaMatrix[0].Add(knightWaveQuotas[i]);
        }
        for (int i2 = 0; i2 < bowmenWaveQuotas.Length; i2++)
        {
            if (bowmenWaveQuotas[i2] < 0) quotaMatrix[1].Add(0);
            else quotaMatrix[1].Add(bowmenWaveQuotas[i2]);
        }
        for (int i3 = 0; i3 < buglerWaveQuotas.Length; i3++)
        {
            if (buglerWaveQuotas[i3] < 0) quotaMatrix[2].Add(0);
            else quotaMatrix[2].Add(buglerWaveQuotas[i3]);
        }

        smallestQuota = Mathf.Min(knightWaveQuotas.Length, Mathf.Min(bowmenWaveQuotas.Length, buglerWaveQuotas.Length)) - 1;
    }

    public void AddOpenSpawnLocation(SpawnLocation newLocation)
    {
        if (GameManager.enabledGameManager.silenceManagers) return;
        openSpawnLocations.Add(newLocation);
    }

    private void RemoveOpenSpawnLocation(SpawnLocation location)
    {
        if (GameManager.enabledGameManager.silenceManagers) return;
        openSpawnLocations.Remove(location);
    }

    public void SpawnWave()
    {
        if (GameManager.enabledGameManager.silenceManagers) return;
        if (!UseNewQuotaSystem) OldQuotaSystem();
        else NewQuotaSystem();
    }

    private void OldQuotaSystem()
    {
        int wave = GameManager.enabledGameManager.waveCount;
        int quota;
        if (wave == 0) quota = 3;
        else quota = wave * 2; //placeholder calculation, we can make this more complex later
        while (quota > 0)
        {
            ChooseLocationAndInstantiate(knightPrefab, ref quota);
        }
        GameManager.enabledGameManager.waveCount++;
    }

    private void NewQuotaSystem()
    {
        int wave = GameManager.enabledGameManager.waveCount;
        int knightQuota;
        int bowmanQuota;
        int buglerQuota;

        if(wave < smallestQuota)
        {
            knightQuota = knightWaveQuotas[wave];
            bowmanQuota = bowmenWaveQuotas[wave];
            buglerQuota = buglerWaveQuotas[wave];
        }
        else
        {
            int waveDiff = wave - smallestQuota;
            knightQuota = knightWaveQuotas[smallestQuota] + waveDiff;
            bowmanQuota = bowmenWaveQuotas[smallestQuota] + waveDiff;
            buglerQuota = buglerWaveQuotas[smallestQuota] + waveDiff;
        }

        while (knightQuota > 0)
        {
            ChooseLocationAndInstantiate(knightPrefab, ref knightQuota);
        }
        while (bowmanQuota > 0)
        {
            ChooseLocationAndInstantiate(bowmenPrefab, ref bowmanQuota);
        }
        while (buglerQuota > 0)
        {
            ChooseLocationAndInstantiate(buglerPrefab, ref buglerQuota);
        }
        GameManager.enabledGameManager.waveCount++;
    }

    private void ChooseLocationAndInstantiate(GameObject prefab, ref int quota)
    {
        SpawnLocation chosenLocation = openSpawnLocations[Random.Range(0, openSpawnLocations.Count)];
        for (int i = 0; i < chosenLocation.prefabCap; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(chosenLocation.spawnBoundsMin.x, chosenLocation.spawnBoundsMax.x) + chosenLocation.transform.position.x,
                                           chosenLocation.transform.position.y,
                                           Random.Range(chosenLocation.spawnBoundsMin.y, chosenLocation.spawnBoundsMax.y) + chosenLocation.transform.position.z);
            GameObject nextSoldier = Instantiate(prefab, spawnPos, Quaternion.identity);
            AIOverseer.overseer.ReportAgentAddition(nextSoldier);
            AIOverseer.overseer.GiveHintToAgent(nextSoldier.GetComponent<NavMeshAgent>());
            quota--;
            if (quota <= 0) break;
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
    }
}
