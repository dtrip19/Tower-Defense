using UnityEngine;

public class AcidRainProjectile : Projectile
{
    new private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (pierce <= 0)
            {
                Destroy(gameObject);
                return;
            }

            enemy.TakeDamage(damage, damageType);
            pierce--;
            if (!enemy.TryGetComponent(out AcidRainDamage acidDamage))
            {
                enemy.gameObject.AddComponent<AcidRainDamage>();
            }
            else
            {
                acidDamage.ticks = 8;
            }

            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
