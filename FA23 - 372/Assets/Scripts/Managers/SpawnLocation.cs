using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    public Vector2 spawnBoundsMin;
    public Vector2 spawnBoundsMax;
    public SpawnLocation[] neighbors;

    public int prefabCap = 1;
    public bool open = true;

    private void Start()
    {
        if (open) SpawnManager.enabledSpawnManager.AddOpenSpawnLocation(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 corner_TL = new Vector3(spawnBoundsMin.x, transform.position.y, spawnBoundsMax.y);
        Vector3 corner_BL = new Vector3(spawnBoundsMin.x, transform.position.y, spawnBoundsMin.y);
        Vector3 corner_TR = new Vector3(spawnBoundsMax.x, transform.position.y, spawnBoundsMax.y);
        Vector3 corner_BR = new Vector3(spawnBoundsMax.x, transform.position.y, spawnBoundsMin.y);

        Gizmos.DrawLine(corner_TL, corner_BL);
        Gizmos.DrawLine(corner_BL, corner_BR);
        Gizmos.DrawLine(corner_BR, corner_TR);
        Gizmos.DrawLine(corner_TR, corner_TL);
    }
}
