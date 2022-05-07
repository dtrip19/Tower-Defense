using UnityEngine;

public class StumpTowerProjectile : Projectile
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
            if (enemy.TryGetComponent(out SapSlow slow))
                slow.ticks = 200;
            else
                enemy.gameObject.AddComponent<SapSlow>();
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
