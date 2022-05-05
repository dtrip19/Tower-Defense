using UnityEngine;

public class AcidRainDamage : MonoBehaviour
{
    public int ticks = 8;
    private Enemy enemy;
    private int timer;

    private void Awake()
    {
        if (TryGetComponent(out Enemy enemy))
            this.enemy = enemy;
        else
            Destroy(this);
    }

    private void FixedUpdate()
    {
        if (timer++ >= 25)
        {
            enemy.TakeDamage(7, DamageType.Elemental);
            ticks--;
            timer = 0;
        }

        if (ticks <= 0)
            Destroy(this);
    }
}
