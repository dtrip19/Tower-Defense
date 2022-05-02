using UnityEngine;
using System.Collections.Generic;

public class HurricaneTowerBehavior : TowerBehaviorBase
{
    private Transform orbitTransform;
    private List<Projectile> projectilesToSlow = new List<Projectile>();

    new private void Awake()
    {
        base.Awake();

        orbitTransform = new GameObject().transform;
        orbitTransform.SetParent(_transform);
        orbitTransform.localPosition = Vector3.zero;
    }

    new private void Update()
    {
        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Projectile);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Projectile proj) && proj.projectileType != ProjectileType.Heavy)
            {
                projectilesToSlow.Add(proj);
                proj.transform.SetParent(orbitTransform);
                //proj.timeDestroy += 3;
            }
        }

        for (int i = projectilesToSlow.Count - 1; i >= 0; i--)
        {
            float newSpeed = projectilesToSlow[i].speed - Time.deltaTime * 3;
            if (newSpeed < 0.25f)
            {
                projectilesToSlow[i].speed = 0;
                projectilesToSlow.Remove(projectilesToSlow[i]);
            }
            else
                projectilesToSlow[i].speed = newSpeed;
        }
    }

    new private void FixedUpdate()
    {
        orbitTransform.Rotate(Vector3.up, 3);

        timer++;
        if (timer >= attackDelay && canShoot)
        {
            timer = 0;
            Shoot();
        }
    }

    protected override void Shoot()
    {
        var spawnPosition = Random.insideUnitSphere * range + _transform.position;
        var proj = Instantiate(bullet).GetComponent<Projectile>();
        proj.transform.position = spawnPosition;
        proj.SetValues(Vector3.zero, DamageType.Normal, Time.time + lifeTime, 0, damage, pierce);

        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                int damage = this.damage;
                if (enemy.attributes.Contains(EnemyAttribute.Flying))
                    damage *= 2;
                enemy.TakeDamage(damage, DamageType.Normal);
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(orbitTransform.gameObject);
    }
}
