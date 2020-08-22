using System.Collections.Generic;
using UnityEngine;

namespace Bases
{
    public class BaseManager : MonoBehaviour
    {
        public static BaseManager Inst;

        private          Base[]     bases;
        private readonly List<Base> activeBases = new List<Base>();

        private void Awake() => Inst = this;

        private void Start()
        {
            bases = FindObjectsOfType<Base>();
            activeBases.AddRange(bases);
        }

        public Base GetRandomActiveBase()
        {
            if(CheckLose())
                return null;

            return activeBases[Random.Range(0, activeBases.Count)];
        }

        public void RemoveFromActiveBases(Base @base)
        {
            if(activeBases.Contains(@base))
                activeBases.Remove(@base);

            CheckLose();
        }

        public void RestoreBases()
        {
            foreach(var @base in bases)
            {
                if(activeBases.Contains(@base))
                    continue;

                @base.Restore();
                activeBases.Add(@base);
            }
        }

        public void ShieldBases()
        {
            foreach(var @base in activeBases)
                @base.Shield();
        }

        private bool CheckLose()
        {
            if(activeBases.Count != 0)
                return false;

            ScoreManager.Inst?.PostScore();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return true;
        }
    }
}