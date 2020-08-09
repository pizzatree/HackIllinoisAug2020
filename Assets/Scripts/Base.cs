using UnityEngine;

public class Base : MonoBehaviour
{
    private bool shielded = false;

    public Vector2 GetPosition() => transform.position;

    public void BlowUp()
    {
        if(shielded)
        {
            shielded = false;
            return;
        }

        // sprite change
        BaseManager.Inst.BaseDestroyed(this);
    }

    public void Restore()
    {
        // full health sprite
        // maybe restore particle effect
    }
}