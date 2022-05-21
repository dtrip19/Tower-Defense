using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMelonTowerBehavior : TowerBehaviorBase
{
    protected override void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.Transform.position = BulletOrigin;
        
        Vector3 destination;
        const int aimAssist = 4;
        if(target.positions.Length - target.PathPositionIndex < aimAssist)
        {
            destination = target.transform.position;
        }
        else
        {
            var expectedLocation = target.positions[target.PathPositionIndex+aimAssist];
            destination = new Vector3(expectedLocation.x, expectedLocation.y + target.Height, expectedLocation.z);
        }
        projectile.SetValues(destination, DamageType, Time.time + lifeTime, bulletSpeed, damage, pierce);
    }
}
