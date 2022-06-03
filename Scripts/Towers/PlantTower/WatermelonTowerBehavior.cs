using UnityEngine;

public class WatermelonTowerBehavior : TowerBehaviorBase
{
    protected override void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.Transform.position = BulletOrigin;
        
        Vector3 destination;
        const int aimAssist = 4;
        if(target.positions.Length - target.PathPositionIndex < aimAssist || !target.canMove)
        {
            destination = target.transform.position;
        }
        else
        {
            var expectedLocation = target.positions[target.PathPositionIndex+aimAssist];
            destination = new Vector3(expectedLocation.x, expectedLocation.y + target.Height, expectedLocation.z);
        }
        projectile.SetValues(destination, DamageType, Time.time + lifeTime, bulletSpeed, damage, pierce);
        projectile.projectileType = ProjectileType.Heavy;
    }
}
