using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public DamageType damageType;
    public float speed;
    public int damage;
    public int pierce = 1;
    private Transform _transform;
    private const int groundLayer = 8;
    private const int unplaceableLayer = 9;

    public Transform Transform => _transform;
    
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        _transform.position += speed / 10 * direction;
    }

    private void OnTriggerEnter(Collider other)
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

    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.collider.gameObject.layer;
        if (layer == unplaceableLayer || layer == groundLayer)
            Destroy(gameObject);
    }
}
