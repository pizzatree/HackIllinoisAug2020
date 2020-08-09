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
    private float highSpawnTime = 3f, lowSpawnTime = 1.2f;

    private float spawnXRange = 9; // make dynamic with camera

    private char? activeEnemyLetter = null;

    [SerializeField]
    private GameObject friendlyMissile = null;
    [SerializeField]
    private Transform friendlySpawnPos = null;

    private int difficulty = 0;
    private int numSpawns = 1;

    private void Awake() => Inst = this;

    private void Start()
    {
        Invoke("SpawnEnemy", 0);
    }

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) && activeEnemyLetter.HasValue))
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

        char variable = (char)Random.Range('a', 'z');
        for(int attempt = 0; attempt < 15 && activeEnemies.ContainsKey(variable); ++attempt)
            variable = (char)Random.Range('a', 'z');

        if(activeEnemies.ContainsKey(variable))
        {
            Invoke("SpawnEnemy", Random.Range(lowSpawnTime, highSpawnTime));
            return;
        }

        var newQuestion = GenerateQuestion();
        var newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity, transform);
        newEnemy.GetComponent<Problem>().AssignProperties(variable, newQuestion, difficulty);

        activeEnemies.Add(variable, newEnemy.GetComponent<Problem>());

        ++numSpawns;
        Invoke("SpawnEnemy", Random.Range(lowSpawnTime, highSpawnTime));
    }

    private iQuestion GenerateQuestion()
    {
        difficulty = numSpawns / 10;

        Debug.Log(difficulty);

        switch(Random.Range(0, 4))
        {
            case 0: return new Add();
            case 1: return new Subtract();
            case 2: return new Multiply();
            case 3: return new Divide();
            default: return new Add();
        }
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
