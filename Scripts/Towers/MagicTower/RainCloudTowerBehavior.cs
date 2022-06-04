using UnityEngine;

public class RainCloudTowerBehavior : TowerBehaviorBase
{
    protected Transform rainCloudTransform;
    protected const int rainCloudHeight = 15;

    protected override DamageType DamageType => DamageType.Elemental;
    protected float ProjectileSpawnLocationRandomOffset => Random.Range(-1.5f, 1.5f);

    new protected void Awake()
    {
        base.Awake();
        rainCloudTransform = Instantiate(Resources.Load<GameObject>("Towers/MagicTower/RainCloud")).GetComponent<Transform>();
        rainCloudTransform.position = new Vector3(_transform.position.x, rainCloudHeight, _transform.position.z);
    }

    new protected void FixedUpdate()
    {
        if (target != null && !target.Equals(null))
        {
            var cloudPosition = rainCloudTransform.position;
            var pathIndex = target.PathPositionIndex + 6;
            if (pathIndex >= target.positions.Length)
                pathIndex = target.positions.Length - 1;
            var expectedPosition = target.positions[pathIndex];
            var targetPosition = new Vector3(expectedPosition.x, rainCloudHeight, expectedPosition.z);
            rainCloudTransform.position = Vector3.Lerp(cloudPosition, targetPosition, .01f);
        }
        
        base.FixedUpdate();
    }

    protected override void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.Transform.position = new Vector3
        {
            x = rainCloudTransform.position.x + ProjectileSpawnLocationRandomOffset,
            y = rainCloudTransform.position.y,
            z = rainCloudTransform.position.z + ProjectileSpawnLocationRandomOffset
        };
        projectile.SetValues(Vector3.down, DamageType, Time.time + lifeTime, bulletSpeed, damage, pierce);
    }

    protected void OnDestroy()
    {
        if (rainCloudTransform != null)
            Destroy(rainCloudTransform.gameObject);
    }
}
