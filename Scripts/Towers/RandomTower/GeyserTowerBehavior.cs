using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserTowerBehavior : TowerBehaviorBase
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
        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                int damage = this.damage;
                if (enemy.Attributes.Contains(EnemyAttribute.Flying))
                    damage *= 2;
                enemy.TakeDamage(damage, DamageType.Normal);
            }
        }

        Enemy newTarget = null;
        int furthestIndex = 0;
        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out Enemy enemy) || enemy.Attributes.Contains(EnemyAttribute.Flying)) continue;

            int pathPositionIndex = enemy.PathPositionIndex;
            if (pathPositionIndex > furthestIndex)
            {
                furthestIndex = pathPositionIndex;
                newTarget = enemy;
            }
        }

        if (newTarget != null)
        {
            var targetPosition = newTarget.positions[furthestIndex] + new Vector3(0, 2, 0);
            colliders = Physics.OverlapSphere(targetPosition, 2, Layers.Enemy);
            foreach (var collider in colliders)
            {
                //collider.gameObject.AddComponent<g>
            }
        }
    }
}
