using UnityEngine;
using System.Collections.Generic;

public class HurricaneTowerBehavior : TowerBehaviorBase
{
    private Transform orbitTransform;
    private List<Projectile> projectilesToSlow = new List<Projectile>();
    private int projectilesOrbiting;

    new private void Awake()
    {
        base.Awake();

        orbitTransform = new GameObject().transform;
        orbitTransform.SetParent(_transform);
        orbitTransform.localPosition = Vector3.zero;
    }

    new private void Update()
    {
        orbitTransform.Rotate(Vector3.up, 90 * Time.deltaTime);

        if (projectilesOrbiting < pierce)
        {
            var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Projectile);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Projectile proj) && proj.projectileType != ProjectileType.Heavy)
                {
                    projectilesToSlow.Add(proj);
                    projectilesOrbiting++;
                    proj.OnDestroyed += DecrementProjectilesOribting;
                    proj.gameObject.layer = 4;
                    proj.transform.SetParent(orbitTransform);
                }
            }
        }

        float deltaTime = Time.deltaTime;
        for (int i = projectilesToSlow.Count - 1; i >= 0; i--)
        {
            float newSpeed = projectilesToSlow[i].speed - deltaTime * 13;
            if (newSpeed < 0.1f)
            {
                projectilesToSlow[i].speed = 0;
                projectilesToSlow.Remove(projectilesToSlow[i]);
            }
            else
                projectilesToSlow[i].speed = newSpeed;
        }

        _transform.Rotate(0,3,0);
        base.Update();
    }

    protected override void Shoot()
    {
        var spawnPosition = Random.insideUnitSphere * range + _transform.position;
        var proj = Instantiate(bullet).GetComponent<Projectile>();
        proj.transform.position = spawnPosition;
        proj.SetValues(Vector3.zero, DamageType.Normal, Time.time + lifeTime, 0, damage, pierce);

        int enemiesHit = 0;
        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);
        foreach (var collider in colliders)
        {
            if (enemiesHit++ > pierce) return;

            if (collider.TryGetComponent(out Enemy enemy))
            {
                int damage = this.damage;
                if (enemy.attributes.Contains(EnemyAttribute.Flying))
                    damage *= 2;
                enemy.TakeDamage(damage, DamageType.Normal);
            }
        }
    }

    private void DecrementProjectilesOribting()
    {
        projectilesOrbiting--;
    }

    private void OnDestroy()
    {
        Destroy(orbitTransform.gameObject);
    }
}
