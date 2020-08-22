using UnityEngine;

namespace Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Sprite healthy = null, 
                                        destroyed = null;

        private SpriteRenderer spriteRenderer,
                               shieldSpriteRenderer;

        private GameObject healParticles;

        private bool shielded = false;

        private void Start()
        {
            spriteRenderer               = GetComponent<SpriteRenderer>();
            shieldSpriteRenderer         = transform.Find("Shield").GetComponent<SpriteRenderer>();
            shieldSpriteRenderer.enabled = false;
            healParticles = Resources.Load<GameObject>("Heal");

        }

        public Vector2 GetPosition() => transform.position;

        public void BlowUp()
        {
            if(shielded)
            {
                shielded                     = false;
                shieldSpriteRenderer.enabled = false;
                return;
            }

            spriteRenderer.sprite = destroyed;
            BaseManager.Inst.RemoveFromActiveBases(this);
        }

        public void Restore()
        {
            spriteRenderer.sprite = healthy;
            Instantiate(healParticles, transform.position + Vector3.up, Quaternion.identity);
        }

        public void Shield()
        {
            shielded                     = true;
            shieldSpriteRenderer.enabled = true;
            
            Instantiate(healParticles, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}