using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public static BaseManager Inst;

    private Base[] bases;
    private List<Base> activeBases = new List<Base>();

    private void Awake() => Inst = this;

    private void Start()
    {
        bases = FindObjectsOfType<Base>();
        activeBases.AddRange(bases);
    }

    public Base GetRandomActiveBase()
    {
        if(activeBases.Count == 0)
        {
            Debug.LogWarning("All bases destroyed, GAME OVER");
            return null;
        }

        var @base = activeBases[Random.Range(0, activeBases.Count)];
        return @base;
    }

    public void BaseDestroyed(Base @base)
    {
        if(activeBases.Contains(@base))
            activeBases.Remove(@base);

        if(activeBases.Count == 0)
            Debug.LogWarning("All bases destroyed, GAME OVER");
    }

    public void BaseRestored(Base @base)
    {
        if(!activeBases.Contains(@base))
            activeBases.Add(@base);
    }
}
