using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the greatest and best script in the world
public class MasterExploder : MonoBehaviour
{
    public static MasterExploder Inst;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip explosion;

    private void Awake()
    {
        Inst = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void aaaAAAAAAAAA()
    {
        audioSource.PlayOneShot(explosion, 1);
    }
}
