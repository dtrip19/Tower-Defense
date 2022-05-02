using UnityEngine;

public class GeyserTowerBehavior : TowerBehaviorBase
{
    private GameObject geyserIndicatorObject;

    new private void Awake()
    {
        base.Awake();
        geyserIndicatorObject = Resources.Load<GameObject>("Towers/RandomTower/GeyserIndicator");
    }

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
                if (enemy.attributes.Contains(EnemyAttribute.Flying))
                    damage *= 2;
                enemy.TakeDamage(damage, DamageType.Normal);
            }
        }

        Enemy newTarget = null;
        int furthestIndex = 0;
        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out Enemy enemy) || enemy.attributes.Contains(EnemyAttribute.Flying)) continue;

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
            var geyserIndicator = Instantiate(geyserIndicatorObject);
            geyserIndicator.transform.position = targetPosition;
            Destroy(geyserIndicator, 0.2f);
            colliders = Physics.OverlapSphere(targetPosition, 2, Layers.Enemy);
            foreach (var collider in colliders)
            {
                collider.gameObject.AddComponent<GeyserTowerKnockup>();
            }
        }
    }
}
