using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Questions;
using Questions.Arithmetic;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyManager em;
        private int          difficulty = 0, numSpawns = 0;
        
        [SerializeField] private GameObject[] enemies        = null,
                                              specialEnemies = null;

        private void Start()
        {
            em = EnemyManager.Inst;
            Invoke(nameof(SpawnEnemy), 0);
        }

        private void SpawnEnemy()
        {
            char variable = GenerateVariable();
            if(em.ActiveEnemies.ContainsKey(variable))
            {
                // If we run out of attempts to find an unused, randomized variable -> STOP, try again later
                Invoke(nameof(SpawnEnemy),
                       Random.Range(Constants.ENEMY_BASE_SPAWN_TIME_LOW, Constants.ENEMY_BASE_SPAWN_TIME_HIGH));
                return;
            }

            var enemy       = GenerateEnemy(out var newDifficulty, out var spawnPoint);
            var newQuestion = GenerateQuestion();
            var newEnemy    = Instantiate(enemy, spawnPoint, Quaternion.identity, transform);
            var problem     = newEnemy.GetComponent<Problem>();
            problem.AssignProperties(variable, newQuestion, newDifficulty);
            em.ActiveEnemies.Add(variable, problem);

            Invoke(nameof(SpawnEnemy),
                   Random.Range(Constants.ENEMY_BASE_SPAWN_TIME_LOW, Constants.ENEMY_BASE_SPAWN_TIME_HIGH));
        }
        
        private char GenerateVariable()
        {
            char variable = (char) Random.Range('a', 'z' + 1);
            for(int attempt = 0; attempt < 15 && em.ActiveEnemies.ContainsKey(variable); ++attempt)
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
    }
}
