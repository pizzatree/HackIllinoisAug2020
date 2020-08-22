using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class FriendlyMissile : MonoBehaviour
{
    [SerializeField] private float explodeDistance = 1f;

    private Transform graphic;
    private Transform targetTransform;
    private Enemy     target;

    public void SetTarget(Enemy enemy)
    {
        target          = enemy;
        targetTransform = enemy.GetComponent<Transform>();
        graphic         = transform.Find("Graphic");
    }

    private void Update()
    {
        if(!target)
            return;

        HandleTransform();

        if(Vector2.Distance(transform.position, targetTransform.position) < explodeDistance)
            Explode();
    }

    private void HandleTransform()
    {
        var currentPos = transform.position;
        Vector3 nextPos = Vector2.up * (2 * Time.deltaTime)
                        + Vector2.MoveTowards(currentPos, targetTransform.position, 3 * Time.deltaTime);

        var rot = nextPos - currentPos;
        graphic.rotation = Quaternion.LookRotation(Vector3.forward, rot);

        transform.position = nextPos;
    }

    private void Explode()
    {
        target.Explode();
        Destroy(gameObject);
    }
}