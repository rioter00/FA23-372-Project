using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager enabledGameManager = null;
    [HideInInspector] public int waveCount = 0;
    [HideInInspector] public int playerHealth = 100;
    [HideInInspector] public int playerScore = 0;


    private void Awake()
    {
        if (enabledGameManager == null) enabledGameManager = this;
        else Destroy(this);
    }

    private void Start()
    {
        GameObject[] allSpawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");
        foreach (GameObject g in allSpawnLocations)
            if (g.GetComponent<SpawnLocation>().open)
                SpawnManager.enabledSpawnManager.AddOpenSpawnLocation(g.GetComponent<SpawnLocation>());
        SpawnManager.enabledSpawnManager.SpawnWave();
    }


     public void OnPlayerWeaponStateChange (GunState state) {
          switch (state) {
              case GunState.READYTOFIRE:
                  AIOverseer.overseer.SignalDodgeToFightingAgents();
                  break;
              default:
                  AIOverseer.overseer.SignalRushToFightingAgents();
                  break;
          }
     }

}
