using UnityEngine;

public class TeslaTowerBehavior : TowerBehaviorBase
{
    protected override void Shoot()
    {
        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);

        int enemiesShot = 0;

        foreach (var collider in colliders)
        {
            if (enemiesShot > 5) return;

            if (!collider.TryGetComponent(out Enemy enemy)) continue;

            var projectile = Instantiate(bullet).GetComponent<Projectile>();
            projectile.damageType = DamageType;
            projectile.Transform.position = BulletOrigin;
            var dirToEnemy = enemy.LineOfSightPosition - BulletOrigin;

            projectile.SetValues(dirToEnemy.normalized, DamageType.Elemental, Time.time + lifeTime, bulletSpeed, damage, pierce);
            enemiesShot++;
        }
    }
}
