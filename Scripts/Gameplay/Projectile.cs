using UnityEngine;

public enum ProjectileType { Normal, Heavy }

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public ProjectileType projectileType;
    [HideInInspector] public DamageType damageType;
    [HideInInspector] public float speed;
    [HideInInspector] public float timeDestroy;
    [HideInInspector] public int damage;
    [HideInInspector] public int pierce = 1;
    [HideInInspector] public bool collidesWithSurfaces = true;

    protected Transform _transform;

    public Transform Transform => _transform;
    
    protected void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    protected void Update()
    {
        if (Time.time > timeDestroy)
        {
            Destroy(gameObject);
        }
    }

    protected void FixedUpdate()
    {
        _transform.position += speed / 10 * direction.normalized;
    }

    public virtual void SetValues(Vector3 direction, DamageType damageType, float timeDestroy, float speed, int damage, int pierce)
    {
        this.direction = direction;
        this.damageType = damageType;
        this.timeDestroy = timeDestroy;
        this.speed = speed;
        this.damage = damage;
        this.pierce = pierce;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (collidesWithSurfaces)
        {
            int layer = other.gameObject.layer;
            if (layer == Layers.UnplaceableRaw || layer == Layers.GroundRaw)
            {
                Destroy(gameObject);
                return;
            }
        }

        if (other.TryGetComponent(out Enemy enemy))
        {
            if (pierce <= 0)
            {
                Destroy(gameObject);
                return;
            }
            enemy.TakeDamage(damage, damageType);
            pierce--;
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual Projectile Shoot(GameObject bullet, DamageType damageType, float timeDestroy, float bulletSpeed, int pierce, Enemy target)
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.gameObject.SetActive(true);//we dont know why. Dont remove this.
        projectile.transform.position = _transform.position;
        var dirToEnemy = target.LineOfSightPosition - _transform.position;

        projectile.SetValues(dirToEnemy.normalized, damageType, timeDestroy, bulletSpeed, damage, pierce);
        return projectile;
    }

    protected virtual bool IsTargetVisible(Enemy enemy, float range)
    {
        Vector3 dirToTarget = (enemy.LineOfSightPosition - _transform.position).normalized;
        return !Physics.Raycast(_transform.position, dirToTarget, out RaycastHit hit, range, Layers.Unplaceable);
    }

    protected Enemy FindFurthestTarget(int range)
    {
        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);

        Enemy newTarget = null;
        int furthestIndex = 0;
        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out Enemy enemy)) continue;

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
