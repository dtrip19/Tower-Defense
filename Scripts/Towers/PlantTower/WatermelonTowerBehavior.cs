using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonTowerBehavior : TowerBehaviorBase
{
        protected override void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.Transform.position = BulletOrigin;
        var dirToEnemy = target.LineOfSightPosition - BulletOrigin;

        projectile.SetValues(target.transform.position, DamageType, Time.time + lifeTime, bulletSpeed, damage, pierce);
    }
}
