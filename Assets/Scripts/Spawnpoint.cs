using System;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public DangerZone dangerZone;
    public Color gizmoColor;
    [SerializeField] private int desiredSpawnCount;
    public int DesiredSpawnCount => desiredSpawnCount;

    private int currentSpawnCount;
    private float timeSinceLastSpawn;

    [SerializeField] private float spawnIntervalSec;
    [SerializeField] private float spawnIntervalOffsetSec;


    public void Awake()
    {
        timeSinceLastSpawn += spawnIntervalOffsetSec;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private bool CanSpawn()
    {
        return currentSpawnCount < desiredSpawnCount &&
               timeSinceLastSpawn >= spawnIntervalSec;
    }

    public bool TickAndCheckSpawn()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (!CanSpawn()) return false;
        timeSinceLastSpawn = 0;
        ++currentSpawnCount;
        return true;
    }
}
