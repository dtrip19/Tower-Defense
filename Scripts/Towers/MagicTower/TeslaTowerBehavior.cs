using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTowerBehavior : TowerBehaviorBase
{
    new protected void Update(){}
    new protected void FixedUpdate()
    {
        timer++;
        if (timer >= attackDelay && canShoot)
        {
            timer = 0;
            Shoot();
        }
    }


    new protected void Shoot()
    {

        var colliders = Physics.OverlapSphere(_transform.position, range, (int)Layers.Enemy);

        int enemiesShot = 0;

        foreach (var collider in colliders)
        {
            if (enemiesShot > 5) return;

            if (!collider.TryGetComponent(out Enemy enemy)) continue;

            var projectile = Instantiate(bullet).GetComponent<Projectile>();
            projectile.damageType = DamageType;
            projectile.Transform.position = BulletOrigin;
            var dirToEnemy = enemy.LineOfSightPosition - BulletOrigin;

            projectile.SetValues(dirToEnemy.normalized, DamageType.Normal, Time.time + lifeTime, bulletSpeed, damage, pierce);
            enemiesShot++;

        }
    }
}
