using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager              Inst;
        public        Dictionary<char, Problem> ActiveEnemies { get; } = new Dictionary<char, Problem>();

        [SerializeField] private GameObject friendlyMissile  = null;
        [SerializeField] private Transform  friendlySpawnPos = null;

        private char? activeEnemyLetter = null;

        private void Awake() => Inst = this;

        private void Update()
        {
            bool acceptButton = Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) ||
                                Input.GetKeyDown(KeyCode.KeypadEnter);
            if(acceptButton)
                Deselect();

            var inputChar = KeyInput.GetKey(true);
            if(inputChar.HasValue && ActiveEnemies.ContainsKey(inputChar.Value))
            {
                Deselect();
                Select(inputChar.Value);
            }
        }

        private void Deselect()
        {
            if(activeEnemyLetter.HasValue)
                ActiveEnemies[activeEnemyLetter.Value].LoseTarget();
            activeEnemyLetter = null;
        }

        public void ForceDeselect(char variable)
        {
            if(activeEnemyLetter.Value == variable)
                activeEnemyLetter = null;
        }

        private void Select(char variable)
        {
            if(!ActiveEnemies.ContainsKey(variable))
                return;
            ActiveEnemies[variable].Target();
            activeEnemyLetter = variable;
        }

        // Called via the enemy objects themselves based on touch/click input
        public void SelectWithTouch(char variable)
        {
            if(activeEnemyLetter.HasValue)
                Deselect();
            if(ActiveEnemies.ContainsKey(variable))
                Select(variable);
        }

        public void LaunchMissile(char variable)
        {
            var missile = Instantiate(friendlyMissile, friendlySpawnPos.position, Quaternion.identity)
                .GetComponent<FriendlyMissile>();

            missile.SetTarget(ActiveEnemies[variable].GetComponent<Enemy>());
        }

        public void RemoveProblem(char variable)
        {
            if(ActiveEnemies.ContainsKey(variable))
                ActiveEnemies.Remove(variable);
        }
    }
}