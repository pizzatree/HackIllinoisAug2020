using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private Sprite healthy = null, destroyed = null;

    [SerializeField]
    SpriteRenderer shieldSprite = null;

    private SpriteRenderer spriteRenderer;

    private bool shielded = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Vector2 GetPosition() => transform.position;

    public void BlowUp()
    {
        if(shielded)
        {
            shielded = false;
            shieldSprite.enabled = false;
            return;
        }

        spriteRenderer.sprite = destroyed;
        BaseManager.Inst.BaseDestroyed(this);
    }

    public void Restore()
    {
        spriteRenderer.sprite = healthy;
        // maybe restore particle effect
    }

    public void Shield()
    {
        // add shield graphic
        shielded = true;
        shieldSprite.enabled = true;
    }
}