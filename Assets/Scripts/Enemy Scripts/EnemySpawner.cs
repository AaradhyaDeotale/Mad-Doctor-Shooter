using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] private GameObject enemyPrefab;
    private GameObject newEnemy;

    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private int maxEnemyCount = 50; // Maximum number of enemies
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private float minSpawnTime = 0.2f, maxSpawnTime = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Start continuously spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Continue spawning enemies indefinitely
        while (true)
        {
            // Check if the maximum enemy count has been reached
            if (spawnedEnemies.Count < maxEnemyCount)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    void SpawnEnemy()
    {
        // Spawn a new enemy
        newEnemy = Instantiate(enemyPrefab, spawnPositions[Random.Range(0, spawnPositions.Length)].position, Quaternion.identity);
        spawnedEnemies.Add(newEnemy);
    }

    public void EnemyDied(GameObject enemy)
    {
        // Remove the dead enemy from the list
        spawnedEnemies.Remove(enemy);
        // Start spawning new enemies if the count is below the maximum
        if (spawnedEnemies.Count < maxEnemyCount)
        {
            StartCoroutine(RespawnEnemy());
        }
    }

    IEnumerator RespawnEnemy()
    {
        // Wait for a short delay before respawning
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        SpawnEnemy();
    }
}
