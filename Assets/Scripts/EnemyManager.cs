using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemyPrefab;
    public List<Spawnpoint> spawnpoints = new List<Spawnpoint>();
    [SerializeField] private HealthManager healthManager;
    
    private int totalEnemyCount;
    private int remainingEnemyCount;
    private readonly List<Enemy> availablePooledEnemies = new();
    private readonly List<Enemy> activeEnemies = new();

    public void Awake()
    {
        foreach (var spawner in spawnpoints)
        {
            totalEnemyCount += spawner.DesiredSpawnCount;
        }

        remainingEnemyCount = totalEnemyCount;
    }

    public void OnEnable()
    {
        SessionManager.I.OnReset += HandleReset;
    }

    public void OnDisable()
    {
        if (SessionManager.I != null) SessionManager.I.OnReset -= HandleReset;
    }

    public void Update()
    {
        TickSpawners();
    }

    public void SpawnEnemy(Spawnpoint spawner)
    {
        Enemy enemy;
        if (availablePooledEnemies.Count > 0)
        {
            enemy = availablePooledEnemies[0];
            availablePooledEnemies.Remove(enemy);
            enemy.transform.position = spawner.transform.position;
        }
        else
        {
            enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity, this.transform);
        }
        enemy.Initialise(spawner.dangerZone, healthManager);
        activeEnemies.Add(enemy);
        enemy.gameObject.SetActive(true);
        enemy.OnDisabled += HandleEnemyDisabled;
    }

    private void TickSpawners()
    {
        if (!SessionManager.I.IsPlaying) return;
        foreach (var spawner in spawnpoints)
        {
            if (!spawner.TickAndCheckSpawn()) continue;
            SpawnEnemy(spawner);
        }
    }
 
    private void HandleEnemyDisabled(Enemy enemy)
    {
        enemy.OnDisabled -= HandleEnemyDisabled;
        availablePooledEnemies.Add(enemy);
        activeEnemies.Remove(enemy);

        if (!SessionManager.I.IsPlaying) return;
        
        remainingEnemyCount -= 1;
        if (remainingEnemyCount <= 0)
        {
            SessionManager.I.WinGame();
        }
    }

    private void HandleReset()
    {
        remainingEnemyCount = totalEnemyCount;

        foreach (var enemy in activeEnemies.ToArray())
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
