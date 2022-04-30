using UnityEngine;

public class HurricaneTowerBehavior : TowerBehaviorBase
{
    private Transform orbitTransform;

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
                proj.speed = 0;
                proj.transform.SetParent(orbitTransform);
                proj.timeDestroy += 3;
            }
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
    }

    private void OnDestroy()
    {
        Destroy(orbitTransform.gameObject);
    }
}
