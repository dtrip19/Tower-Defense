using UnityEngine;

public sealed class RandomTowerBehavior : TowerBehaviorBase
{
    new private void Update() { }

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
        var dir = new Vector3(Random.Range(-1f, 1f),Random.Range(-0.2f, 1f),Random.Range(-1f, 1f));

        projectile.SetValues(dir.normalized, DamageType.Normal, Time.time + lifeTime, bulletSpeed, damage, pierce);
    }
}
