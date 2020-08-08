using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public static BaseManager Inst;

    private Base[] bases;
    private List<Base> activeBases = new List<Base>();

    private Vector2 emptyPos;

    private void Awake() => Inst = this;

    private void Start()
    {
        bases = FindObjectsOfType<Base>();
        activeBases.AddRange(bases);

        emptyPos = transform.Find("EmptyPos").position;
    }

    public Vector2 GetRandomActiveBase()
    {
        if(activeBases.Count == 0)
        {
            // Signal Game Over
            return emptyPos;
        }

        var @base = activeBases[Random.Range(0, activeBases.Count)];
        return @base.GetPosition();
    }

    public void BaseDestroyed(Base @base)
    {
        activeBases.Remove(@base);

        if(activeBases.Count == 0)
            ; // signal game over
    }

    public void BaseRestored(Base @base)
    {
        activeBases.Add(@base);
    }
}
