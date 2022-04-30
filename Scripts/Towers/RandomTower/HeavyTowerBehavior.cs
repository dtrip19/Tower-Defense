using UnityEngine;

public class HeavyTowerBehavior : TowerBehaviorBase
{
    new private void FixedUpdate()
    {
        timer++;
        if (timer >= attackDelay && canShoot)
        {
            timer = 0;
            Shoot();
        }
    }

    protected override void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.Transform.position = BulletOrigin;
        var dir = new Vector3(Random.Range(-0.35f, 0.35f), Random.Range(1f, 1f), Random.Range(-0.35f, 0.35f));

        projectile.SetValues(dir.normalized, DamageType.Normal, Time.time + lifeTime, bulletSpeed, damage, pierce);
    }
}
