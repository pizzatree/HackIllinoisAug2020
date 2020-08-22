using System.Collections.Generic;
using Questions;
using Questions.Arithmetic;
using UnityEngine;
using Utilities;

namespace Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Inst;

        [SerializeField] private GameObject[] enemies        = null,
                                              specialEnemies = null;

        [SerializeField] private GameObject friendlyMissile  = null;
        [SerializeField] private Transform  friendlySpawnPos = null;

        private readonly Dictionary<char, Problem> activeEnemies = new Dictionary<char, Problem>();

        private int   difficulty        = 0, numSpawns = 0;
        private char? activeEnemyLetter = null;

        private void Awake() => Inst = this;
        private void Start() => Invoke("SpawnEnemy", 0);

        private void Update()
        {
            bool acceptButton = Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) ||
                                Input.GetKeyDown(KeyCode.KeypadEnter);
            if(acceptButton)
                Deselect();

            var inputChar = KeyInput.GetKey(true);
            if(inputChar.HasValue && activeEnemies.ContainsKey(inputChar.Value))
            {
                Deselect();
                Select(inputChar.Value);
            }
        }

        private void Deselect()
        {
            if(activeEnemyLetter.HasValue)
                activeEnemies[activeEnemyLetter.Value].LoseTarget();
            activeEnemyLetter = null;
        }

        public void ForceDeselect(char variable)
        {
            if(activeEnemyLetter == variable)
                activeEnemyLetter = null;
        }

        private void Select(char variable)
        {
            if(!activeEnemies.ContainsKey(variable))
                return;
            activeEnemies[variable].Target();
            activeEnemyLetter = variable;
        }

        // Called via the enemy objects themselves based on touch/click input
        public void SelectWithTouch(char variable)
        {
            if(activeEnemyLetter.HasValue)
                Deselect();
            if(activeEnemies.ContainsKey(variable))
                Select(variable);
        }

        private void SpawnEnemy()
        {
            char variable = GenerateVariable();
            if(activeEnemies.ContainsKey(variable))
            {
                // If we run out of attempts to find an unused, randomized variable -> STOP, try again later
                Invoke("SpawnEnemy",
                       Random.Range(Constants.ENEMY_BASE_SPAWN_TIME_LOW, Constants.ENEMY_BASE_SPAWN_TIME_HIGH));
                return;
            }

            var enemy       = GenerateEnemy(out var newDifficulty, out var spawnPoint);
            var newQuestion = GenerateQuestion();
            var newEnemy    = Instantiate(enemy, spawnPoint, Quaternion.identity, transform);
            var problem     = newEnemy.GetComponent<Problem>();
            problem.AssignProperties(variable, newQuestion, newDifficulty);
            activeEnemies.Add(variable, problem);

            Invoke("SpawnEnemy",
                   Random.Range(Constants.ENEMY_BASE_SPAWN_TIME_LOW, Constants.ENEMY_BASE_SPAWN_TIME_HIGH));
        }

        private char GenerateVariable()
        {
            char variable = (char) Random.Range('a', 'z' + 1);
            for(int attempt = 0; attempt < 15 && activeEnemies.ContainsKey(variable); ++attempt)
                variable = (char) Random.Range('a', 'z' + 1);

            return variable;
        }

        private GameObject GenerateEnemy(out int newDifficulty, out Vector2 spawnPoint)
        {
            GameObject enemy;
            bool spawnSpecial = numSpawns++ % Constants.ENEMY_SPECIAL_SPAWN_RATE ==
                                Constants.ENEMY_SPECIAL_SPAWN_RATE - 1;
            if(spawnSpecial) // spawn a special enemy
            {
                spawnPoint = new Vector2(10, Random.Range(-1f, 3f));
                enemy      = specialEnemies[Random.Range(0,    specialEnemies.Length)];
            }
            else
            {
                spawnPoint.x = Random.Range(-Constants.ENEMY_HORIZONTAL_SPAWN_RANGE,
                                            Constants.ENEMY_HORIZONTAL_SPAWN_RANGE);
                spawnPoint.y = Constants.ENEMY_VERTICAL_SPAWN_HEIGHT;
                enemy        = enemies[Random.Range(0, enemies.Length)];
            }

            newDifficulty = (spawnSpecial) ? difficulty + 5 : difficulty;

            return enemy;
        }

        private IQuestion GenerateQuestion()
        {
            difficulty = ScoreManager.Inst.NumSuccessful / Constants.DIFFICULTY_INCREASE_RATE;
            switch(Random.Range(0, 4))
            {
                case 0:  return new Add();
                case 1:  return new Subtract();
                case 2:  return new Multiply();
                case 3:  return new Divide();
                default: return new Add();
            }
        }

        public void LaunchMissile(char variable)
        {
            var missile = Instantiate(friendlyMissile, friendlySpawnPos.position, Quaternion.identity)
                .GetComponent<FriendlyMissile>();

            missile.SetTarget(activeEnemies[variable].GetComponent<Enemy>());
        }

        public void RemoveProblem(char variable)
        {
            if(activeEnemies.ContainsKey(variable))
                activeEnemies.Remove(variable);
        }
    }
}