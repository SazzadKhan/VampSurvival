using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
 public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 1f;
    public int maxEnemies = 100;
    public float difficultyScalingInterval = 30f;
    public float difficultyScalingFactor = 0.9f;

    private Transform player;
    private int currentEnemies = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(IncreaseDifficulty());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemies < maxEnemies)
            {
                Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
                Vector3 spawnPosition = player.position + new Vector3(randomCircle.x, 0, randomCircle.y);
                
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                currentEnemies++;

                enemy.GetComponent<EnemyBehaviour>().moveSpeed += Random.Range(-0.5f, 0.5f); // Add some variety
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyScalingInterval);
            spawnInterval *= difficultyScalingFactor;
            maxEnemies = Mathf.Min(maxEnemies + 10, 300); // Increase max enemies, cap at 300
        }
    }

    public void EnemyDied()
    {
        currentEnemies--;
    }
}
