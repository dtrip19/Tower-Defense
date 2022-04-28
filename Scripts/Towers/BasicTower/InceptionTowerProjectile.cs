using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InceptionTowerProjectile : Projectile
{
    private int timer;
    private Enemy target;
    private const float range = 10;
    private Transform _transform;
    public GameObject bullet;
    public int attackDelay;
    public float lifeTime;
    new private void Awake()
    {
        base.Awake();
        _transform = transform;
    }

    private void Update()
    {
        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);

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

    new private void FixedUpdate()
    {

        base.FixedUpdate();

        timer++;

        if (timer >= attackDelay && target != null)
        {
            timer = 0;
            Shoot();
        }
    }

    private bool IsTargetVisible(Enemy enemy)
    {
        Vector3 dirToTarget = (enemy.LineOfSightPosition - _transform.position).normalized;
        return !Physics.Raycast(_transform.position, dirToTarget, out RaycastHit hit, range, Layers.Unplaceable);
    }

    protected virtual void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<InceptionTowerProjectile>();
        projectile.damageType = DamageType.Normal;
        projectile.Transform.position = _transform.position;
        var dirToEnemy = target.LineOfSightPosition - _transform.position;

        projectile.attackDelay = attackDelay + 5;
        projectile.direction = dirToEnemy.normalized;
        projectile.speed = speed + 1;
        projectile.damage = damage;
        projectile.pierce = pierce;
        Destroy(projectile.gameObject, lifeTime);
    }
}
