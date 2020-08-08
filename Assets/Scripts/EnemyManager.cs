using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    private Dictionary<char, Problem> activeEnemies
        = new Dictionary<char, Problem>();

    [SerializeField]
    private float startingSpawnTime = 3f;
    private float spawnTime;

    private float spawnXRange = 9; // make dynamic with camera

    private void Start()
    {
        spawnTime = startingSpawnTime;
        Invoke("SpawnEnemy", 0);
    }

    private void SpawnEnemy()
    {
        var spawnPoint = new Vector2((Random.Range(-spawnXRange, spawnXRange)), 7);
        var enemy = enemies[Random.Range(0, enemies.Length)];

        char variable = (char)Random.Range('a', 'z');
        while(activeEnemies.ContainsKey(variable))
            variable = (char)Random.Range('a', 'z');

        var newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        newEnemy.GetComponent<Problem>().AssignProperties(variable, new Add(), 0); // GENERATE A QUESTION

        activeEnemies.Add(variable, newEnemy.GetComponent<Problem>());

        Invoke("SpawnEnemy", spawnTime);
    }
}
