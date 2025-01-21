using System;
using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemyPrefab;
    public List<Spawnpoint> spawnpoints = new List<Spawnpoint>();
    [SerializeField] private HealthManager healthManager;

    public void SpawnEnemy(Spawnpoint spawner)
    {
        // TODO: Add pooling system to reuse enemies
        var enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity, this.transform);
        enemy.Initialise(spawner.dangerZone, healthManager);
    }

    public void Update()
    {
        foreach (var spawner in spawnpoints)
        {
            if (!spawner.TickAndCheckSpawn()) continue;
            SpawnEnemy(spawner);
        }
    }
}
