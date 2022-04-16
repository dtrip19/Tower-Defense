using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBehaviorBase : MonoBehaviour
{
    #region variables
    protected Transform _transform;
    protected Enemy target;
    public GameObject bullet;
    public float range;
    public int attackDelay;
    public int damage;
    public int timer;
    public int enemyLayer = 1 << 11;
    public int unplaceableLayer = 1 << 9;
    public bool canShoot = false;
    public float bulletSpeed;
    public float lifeTime;
    public float bulletOriginHeight;

    protected virtual DamageType DamageType => DamageType.Normal;
    protected Vector3 BulletOrigin
    {
        get
        {
            var offset = bulletOriginHeight;
            return new Vector3(_transform.position.x, _transform.position.y + offset, _transform.position.z);
        }
    }

    #endregion 

    protected virtual void Awake()
    {
        _transform = transform;
    }

    protected virtual void Update()
    {
        if (target != null && !target.Equals(null))
        {
            if (Vector3.Distance(target.Transform.position, _transform.position) > range || !IsTargetVisible(target))
                target = null;
            else
                return;
        }

        var colliders = Physics.OverlapSphere(_transform.position, range, enemyLayer);

        Enemy newTarget = null;
        int furthestIndex = 0;
        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out Enemy enemy)) continue;

            int pathPositionIndex = enemy.PathPositionIndex;
            if (pathPositionIndex > furthestIndex && IsTargetVisible(enemy))
            {
                furthestIndex = pathPositionIndex;
                newTarget = enemy;
            }
        }
        target = newTarget;
    }

    protected void FixedUpdate()
    {
        timer++;
        if (timer >= attackDelay && target != null && canShoot)
        {
            timer = 0;
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.damageType = DamageType;
        projectile.Transform.position = BulletOrigin;
        var dirToEnemy = target.LineOfSightPosition - BulletOrigin;

        projectile.direction = dirToEnemy.normalized;
        projectile.speed = bulletSpeed;
        projectile.damage = damage;
        Destroy(projectile.gameObject, lifeTime / 10);
    }

    protected virtual bool IsTargetVisible(Enemy enemy)
    {
        Vector3 bulletOrigin = new Vector3(_transform.position.x, _transform.position.y + bulletOriginHeight, _transform.position.z);
        Vector3 dirToTarget = (enemy.LineOfSightPosition - bulletOrigin).normalized;
        return !Physics.Raycast(bulletOrigin, dirToTarget, out RaycastHit hit, range, unplaceableLayer);
    }

}