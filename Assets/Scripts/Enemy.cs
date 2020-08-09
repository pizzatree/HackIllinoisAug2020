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
    private Vector3 targetPos;

    bool moving = true;

    private void Start()
    {
        target = BaseManager.Inst.GetRandomActiveBase();
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
            EnemyManager.Inst.ProblemLost(GetComponent<Problem>().Variable);
            target.BlowUp();
            // explosion graphic
            Destroy(gameObject);
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
        Destroy(gameObject);
    }
}
