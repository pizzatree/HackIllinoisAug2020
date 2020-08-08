using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private bool problemStarted = false;
    private char activeEnemyLetter = default;

    private void Start()
    {
        spawnTime = startingSpawnTime;
        Invoke("SpawnEnemy", 0);
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) 
        {
            Debug.Log("fuck");
            activeEnemies[activeEnemyLetter].Deselect();
            problemStarted = false;
        }
        foreach (var enemy in activeEnemies)
        {
            if (!problemStarted && Input.GetKeyDown(CharToKeyCode(enemy.Key)))
            {
                enemy.Value.Listening = true;
                problemStarted = true;
                activeEnemyLetter = enemy.Key;
            }
        }
	}

	private void SpawnEnemy()
    {
        var spawnPoint = new Vector2((Random.Range(-spawnXRange, spawnXRange)), 7);
        var enemy = enemies[Random.Range(0, enemies.Length)];

        char variable = (char)Random.Range('a', 'z');
        while(activeEnemies.ContainsKey(variable))
            variable = (char)Random.Range('a', 'z');

        var newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        newEnemy.GetComponent<Problem>().AssignVariable(variable);
        activeEnemies.Add(variable, newEnemy.GetComponent<Problem>());

        Invoke("SpawnEnemy", spawnTime);
    }

    private KeyCode CharToKeyCode(char letter)
	{
        switch(letter)
		{
            case 'a': return KeyCode.A;
            case 'b': return KeyCode.B;
            case 'c': return KeyCode.C;
            case 'd': return KeyCode.D;
            case 'e': return KeyCode.E;
            case 'f': return KeyCode.F;
            case 'g': return KeyCode.G;
            case 'h': return KeyCode.H;
            case 'i': return KeyCode.I;
            case 'j': return KeyCode.J;
            case 'k': return KeyCode.K;
            case 'l': return KeyCode.L;
            case 'm': return KeyCode.M;
            case 'n': return KeyCode.N;
            case 'o': return KeyCode.O;
            case 'p': return KeyCode.P;
            case 'q': return KeyCode.Q;
            case 'r': return KeyCode.R;
            case 's': return KeyCode.S;
            case 't': return KeyCode.T;
            case 'u': return KeyCode.U;
            case 'v': return KeyCode.V;
            case 'w': return KeyCode.W;
            case 'x': return KeyCode.X;
            case 'y': return KeyCode.Y;
            case 'z': return KeyCode.Z;
        }
        return KeyCode.Space;
	}
}
