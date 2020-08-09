using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Inst;

    [SerializeField]
    private GameObject[] enemies = null;

    private Dictionary<char, Problem> activeEnemies
        = new Dictionary<char, Problem>();

    [SerializeField]
    private float startingSpawnTime = 3f;
    private float spawnTime;

    private float spawnXRange = 9; // make dynamic with camera

    private char? activeEnemyLetter = null;

    [SerializeField]
    private GameObject friendlyMissile = null;
    [SerializeField]
    private Transform friendlySpawnPos = null;

    private void Awake() => Inst = this;

    private void Start()
    {
        spawnTime = startingSpawnTime;
        Invoke("SpawnEnemy", 0);
    }

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) && activeEnemyLetter.HasValue)
        {
            Deselect();
        }

        for(char letter = 'a'; letter <= 'z'; ++letter)
        {
            if(Input.GetKeyDown(letter.ToString()) && activeEnemies.ContainsKey(letter))
            {
                if(activeEnemyLetter.HasValue && activeEnemyLetter.Value != letter)
                    Deselect();

                Select(letter);
            }
        }
    }

    private void Deselect()
    {
        activeEnemies[activeEnemyLetter.Value].Deselect();
        activeEnemyLetter = null;
    }

    private void Select(char letter)
    {
        activeEnemies[letter].Select();
        activeEnemyLetter = letter;
    }

    private void SpawnEnemy()
    {
        var spawnPoint = new Vector2((Random.Range(-spawnXRange, spawnXRange)), 7);
        var enemy = enemies[Random.Range(0, enemies.Length)];

        int attempts = 0;
        char variable = (char)Random.Range('a', 'z');
        while(activeEnemies.ContainsKey(variable))
        {
            if(attempts++ > 15)
            {
                Invoke("SpawnEnemy", spawnTime);
                return;
            }
            variable = (char)Random.Range('a', 'z');
        }

        var newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity, transform);
        newEnemy.GetComponent<Problem>().AssignProperties(variable, new Add(), 0); // GENERATE A QUESTION

        activeEnemies.Add(variable, newEnemy.GetComponent<Problem>());

        Invoke("SpawnEnemy", spawnTime);
    }

    public void ProblemAccepted(char letter)
    {
        var missile =
            Instantiate(friendlyMissile, friendlySpawnPos.position, Quaternion.identity)
            .GetComponent<FriendlyMissile>();

        missile.SetTarget(activeEnemies[letter].GetComponent<Enemy>());

        activeEnemies.Remove(letter);
    }

    public void ProblemLost(char letter)
        => activeEnemies.Remove(letter);

}
