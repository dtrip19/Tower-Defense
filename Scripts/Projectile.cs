using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _transform;
    public float speed;
    public int damage;
    public int pierce = 1;
    public Vector3 direction;

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
            enemy.TakeDamage(damage);
            pierce--;
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
