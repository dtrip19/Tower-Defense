using UnityEngine;

public enum ProjectileType { Normal, Heavy }

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public ProjectileType projectileType;
    public DamageType damageType;
    public float speed;
    public float timeDestroy;
    public int damage;
    public int pierce = 1;
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

    protected void OnCollisionEnter(Collision collision)
    {
        int layer = collision.collider.gameObject.layer;
        if (layer == Layers.UnplaceableRaw || layer == Layers.GroundRaw)
        {
            Destroy(gameObject);
        }
    }
}
