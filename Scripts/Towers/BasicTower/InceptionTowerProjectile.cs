using UnityEngine;

public class InceptionTowerProjectile : Projectile
{
    public GameObject bullet;
    public int attackDelay;
    public float lifeTime;
    public int generation = 1;
    private Enemy target;
    private int timer;
    private const float range = 10;

    new private void Update()
    {
        base.Update();

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

    public void SetValues(Vector3 direction, DamageType damageType, float speed, int damage, int pierce, int generation)
    {
        this.direction = direction;
        this.damageType = damageType;
        this.speed = speed;
        this.damage = damage;
        this.pierce = pierce;
        this.generation = generation;
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/InceptionGen" + generation);
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

        projectile.SetValues(dirToEnemy.normalized, DamageType.Normal, speed + 1, damage, pierce, generation + 1);
        Destroy(projectile.gameObject, lifeTime);
    }
}
