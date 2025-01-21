using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemyPrefab;
    private List<Enemy> enemyList = new List<Enemy>();
    public List<Spawnpoint> spawnpoints = new List<Spawnpoint>();
    [SerializeField] private HealthManager healthManager;
    private int totalEnemyNumberForLevel;

    public void SpawnEnemy(Spawnpoint spawner)
    {
        Enemy enemy;
        if (enemyList.Count > 0)
        {
            enemy = enemyList[0];
            enemyList.Remove(enemy);
            enemy.transform.position = spawner.transform.position;
        }
        else
        {
            enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity, this.transform);
        }
        enemy.Initialise(spawner.dangerZone, healthManager);
        enemy.gameObject.SetActive(true);
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
        enemyList.Add(enemy);
    }
}
