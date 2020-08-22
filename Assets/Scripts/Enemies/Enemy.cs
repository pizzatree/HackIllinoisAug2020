using Audio;
using Bases;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int pointValue = 20;

        [SerializeField] private float speed             = 1,
                                       distanceToExplode = 0.5f;

        [SerializeField] private GameObject explosion = null;
        [SerializeField] private AudioClip  loop      = null;

        protected Base        Target;
        protected AudioSource AudioSource;
        protected Vector3     TargetPos = new Vector3(0, -3, 0);
        
        private   bool        moving    = true;

        protected virtual void Start()
        {
            AudioSource       = GetComponent<AudioSource>();
            AudioSource.pitch = Random.Range(0.95f, 1.05f);

            Target = BaseManager.Inst.GetRandomActiveBase();
            if(Target)
                TargetPos = Target.GetPosition();

            var rot = Quaternion.Euler(0, 0, -180) * (TargetPos - transform.position);
            transform.Find("Graphic").rotation = Quaternion.LookRotation(Vector3.forward, rot);
        }

        private void Update()
        {
            if(moving)
                transform.position = Vector2.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, TargetPos) < distanceToExplode)
            {
                var variable = GetComponent<Problem>().Variable;
                EnemyManager.Inst?.RemoveProblem(variable);
                EnemyManager.Inst?.ForceDeselect(variable);
                Target?.BlowUp();
                Explode();
            }

            if(!AudioSource.isPlaying)
            {
                AudioSource.clip = loop;
                AudioSource.Play();
            }
        }

        protected virtual void SolutionAccepted()
        {
            moving = false;
            ScoreManager.Inst.AddScore(pointValue);
        }

        public void Explode()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            MasterExploder.Inst?.AaaAaaaaaaaa();
            Destroy(gameObject);
        }
    }
}