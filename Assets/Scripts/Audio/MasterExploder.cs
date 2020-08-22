using UnityEngine;

// This is the greatest and best script in the world
namespace Audio
{
    public class MasterExploder : MonoBehaviour
    {
        public static MasterExploder Inst;
        
        [SerializeField]
        private AudioClip explosion = null, music = null;

        private AudioSource sfxSource, bgmSource;

        private void Awake()
        {
            if(Inst != null && Inst != this)
            {
                Destroy(this);
                return;
            }
            else
            {
                Inst = this;
                DontDestroyOnLoad(this);
            }
        }

        private void Start()
        {
            sfxSource = gameObject.AddComponent<AudioSource>();

            bgmSource        = gameObject.AddComponent<AudioSource>();
            bgmSource.loop   = true;
            bgmSource.clip   = music;
            bgmSource.volume = 0.25f;
            bgmSource.Play();
        }

        public void AaaAaaaaaaaa()
        {
            sfxSource.PlayOneShot(explosion, 1);
        }
    }
}