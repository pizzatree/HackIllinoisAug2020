using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private int pointValue = 20;
    private Vector3 target;

    private void Start()
    {
        target = BaseManager.Inst.GetRandomActiveBase();

        var rot = Quaternion.Euler(0, 0, -180) * (target - transform.position);
        transform.Find("Graphic").rotation = Quaternion.LookRotation(Vector3.forward, rot);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void ShipDestroyed()
    {
        ScoreManager.Inst.AddScore(pointValue);
    }
}
