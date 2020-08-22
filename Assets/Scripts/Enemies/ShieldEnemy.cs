using Audio;
using Bases;
using UnityEngine;

namespace Enemies
{
    public class ShieldEnemy : Enemy
    {
        protected override void Start()
        {
            var position = transform.position;
            TargetPos = new Vector3(-position.x, position.y);
            Target    = null;        
            
            AudioSource       = GetComponent<AudioSource>();
            AudioSource.pitch = Random.Range(0.95f, 1.05f);
        }

        protected override void SolutionAccepted()
        {
            base.SolutionAccepted();
            BaseManager.Inst?.ShieldBases();
        }
    }
}