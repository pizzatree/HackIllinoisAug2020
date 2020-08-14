using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1, distanceToExplode = 0.5f;
    [SerializeField]
    private int pointValue = 20;
    private Base target;
    private Vector3 targetPos = new Vector3(0, -3, 0);

    bool moving = true;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip loop = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        target = BaseManager.Inst.GetRandomActiveBase();
        if(target)
            targetPos = target.GetPosition();

        var rot = Quaternion.Euler(0, 0, -180) * (targetPos - transform.position);
        transform.Find("Graphic").rotation = Quaternion.LookRotation(Vector3.forward, rot);
    }

    private void Update()
    {
        if(moving)
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    
        if(Vector2.Distance(transform.position, targetPos) < distanceToExplode)
        {
            var variable = GetComponent<Problem>().Variable;
            EnemyManager.Inst.RemoveProblem(variable);
            EnemyManager.Inst.ForceDeselect(variable);
            target?.BlowUp();
            MasterExploder.Inst.aaaAAAAAAAAA();
            // explosion graphic
            Destroy(gameObject);
        }

        if(!audioSource.isPlaying)
        {
            audioSource.clip = loop;
            audioSource.Play();
        }
    }

    private void ShipDestroyed()
    {
        moving = false;
        ScoreManager.Inst.AddScore(pointValue);
    }

    public void Explode()
    {
        // explosion graphic
        MasterExploder.Inst.aaaAAAAAAAAA();
        Destroy(gameObject);
    }
}
