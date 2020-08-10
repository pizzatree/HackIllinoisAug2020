using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the greatest and best script in the world
public class MasterExploder : MonoBehaviour
{
    public static MasterExploder Inst;

    private AudioSource audioSource;

    AudioSource musicSource;
    [SerializeField]
    private AudioClip explosion;
    [SerializeField]
    private AudioClip music;

    private void Awake()
    {
        Inst = this;
        audioSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
    }

	private void Update()
	{
        if (!musicSource.isPlaying) musicSource.Play();
	}

	public void aaaAAAAAAAAA()
    {
        audioSource.PlayOneShot(explosion, 1);
    }
}
