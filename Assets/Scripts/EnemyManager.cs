using System;
using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemyPrefab;
    public List<Spawnpoint> spawnpoints = new List<Spawnpoint>();
    [SerializeField] private HealthManager healthManager;
    private int totalEnemyNumberForLevel;

    public void SpawnEnemy(Spawnpoint spawner)
    {
        // TODO: Add pooling system to reuse enemies
        var enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity, this.transform);
        enemy.Initialise(spawner.dangerZone, healthManager);
        enemy.OnDisabled += HandleEnemyDisabled;
    }

    public void Awake()
    {
        foreach (var spawner in spawnpoints)
        {
            totalEnemyNumberForLevel += spawner.DesiredSpawnCount;
        }
    }

    public void Update()
    {
        foreach (var spawner in spawnpoints)
        {
            if (!spawner.TickAndCheckSpawn()) continue;
            SpawnEnemy(spawner);
        }
    }

    private void HandleEnemyDisabled(Enemy enemy)
    {
        enemy.OnDisabled -= HandleEnemyDisabled;
        totalEnemyNumberForLevel -= 1;
        if (totalEnemyNumberForLevel <= 0)
        {
            Debug.Log("Win");
            Time.timeScale = 0;
        }
    }
}
