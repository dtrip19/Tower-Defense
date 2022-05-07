using UnityEngine;

public class DragonTowerBehavior : TowerBehaviorBase
{
    new protected void Awake()
    {
        base.Awake();
        _transform.position += new Vector3(0, 5, 0);
        _transform.GetChild(0).localPosition = new Vector3(0, -5, 0);
    }

    protected override void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.Transform.position = BulletOrigin;
        var dirToEnemy = (target.LineOfSightPosition - BulletOrigin).normalized;
        var fireDirection = dirToEnemy + Random.insideUnitSphere * .2f;

        projectile.SetValues(fireDirection, DamageType, Time.time + lifeTime, bulletSpeed, damage, pierce);
    }
}