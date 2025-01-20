using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Spawnpoint> spawnpoints = new List<Spawnpoint>();

    void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        // TODO: Add pooling system to reuse enemies
        var enemy = Instantiate(enemyPrefab, spawnpoints[0].transform.position, Quaternion.identity, this.transform).GetComponent<Enemy>();
        enemy.Initialise(spawnpoints[0].dangerZone);
    }

}
