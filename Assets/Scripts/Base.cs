using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    private int curHealth;

    private void Start()
    {
        curHealth = maxHealth;
    }

    public Vector2 GetPosition() => transform.position;

    public void TakeHit()
    {
        if(--curHealth <= 0)
            BlowUp();

        // change sprite based on % health remaining
    }

    public void Restore()
    {
        curHealth = maxHealth;
        // full health sprite
        // maybe restore particle effect
    }

    private void BlowUp()
    {
        // sprite change
        // explosion effect
        BaseManager.Inst.BaseDestroyed(this);
    }
}