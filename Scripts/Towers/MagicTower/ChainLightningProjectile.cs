using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningProjectile : Projectile
{
    public GameObject projectile;
    private float originalSpeed;
    bool hasCollided = false;
    public  List<Enemy> enemiesHit = new List<Enemy>();

    new void Update()
    {
        if(enemiesHit.Count >0 ) 
        {
            var lastEnemy = enemiesHit[enemiesHit.Count-1];
            if(hasCollided && !lastEnemy.Equals(null))
            {
                _transform.position = lastEnemy.LineOfSightPosition;
            }
        }
        base.Update();
    }

    new void OnTriggerEnter(Collider other)
    {
        if (hasCollided) return;
        if (other.TryGetComponent(out Enemy enemy) && !enemiesHit.Contains(enemy))
        {
            enemiesHit.Add(enemy);
            originalSpeed = speed;
            speed = 0;
            Destroy(gameObject,0.15f);
            enemy.TakeDamage(damage, damageType);
            hasCollided = true;
        }
    }

    private void OnDestroy()
    {
        if(pierce <= 1 || !hasCollided) return;

        var target = FindFurthestTarget(4);
        
        if(target != null)
        {
            var newProjectile = Shoot(projectile,DamageType.Elemental,Time.time + 4, originalSpeed, pierce - 1, target);
            if(newProjectile is ChainLightningProjectile newerProjectile)
            {
                newerProjectile.enemiesHit = enemiesHit;
            }
        }
        
    }

    new private Enemy FindFurthestTarget(int range)
    {
        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);

        Enemy newTarget = null;
        int furthestIndex = 0;
        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out Enemy enemy) || enemiesHit.Contains(enemy)) continue;

            int pathPositionIndex = enemy.PathPositionIndex;
            if (pathPositionIndex > furthestIndex && IsTargetVisible(enemy, range))
            {
                furthestIndex = pathPositionIndex;
                newTarget = enemy;
            }
        }
        return newTarget;
    }
}
