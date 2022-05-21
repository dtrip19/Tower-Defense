using UnityEngine;

public class WatermelonTowerProjectile : Projectile
{
    private Vector3 xzSpeed;
    private float gravity = .03f;
    private float vSpeed;
    [SerializeField] protected GameObject explosionObject;
    [SerializeField] protected float explosionRadius;
    [SerializeField] protected int explosionDamage;

    protected void OnCollisionEnter(Collision collision)
    {
        print("collision");
        int layer = collision.collider.gameObject.layer;
        if (layer == Layers.UnplaceableRaw || layer == Layers.GroundRaw)
        {
            var explosionTransform = Instantiate(explosionObject).transform;
            explosionTransform.position = _transform.position;
            Destroy(explosionTransform.gameObject, 0.2f);

            var colliders = Physics.OverlapSphere(_transform.position, explosionRadius, Layers.Enemy);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(explosionDamage, DamageType.Explosive);
                }
            }
            Destroy(gameObject);
        }
    }    
    new private void FixedUpdate()
    {
        vSpeed -= 2*gravity;
        _transform.position += new Vector3(xzSpeed.x, vSpeed, xzSpeed.z);
    }

    public override void SetValues(Vector3 destination, DamageType damageType, float timeDestroy, float speed, int damage, int pierce)
    {
        this.damageType = damageType;
        this.timeDestroy = timeDestroy;
        this.damage = damage;
        this.pierce = pierce;
        var xzDestination = new Vector3(destination.x, 0, destination.z);
        var xzPostion = new Vector3(_transform.position.x, 0, _transform.position.z);
        var distToDestination = Vector3.Distance(xzPostion, xzDestination);
        var dirToDestination = xzDestination - _transform.position;

        gravity *= speed * speed;
        vSpeed = speed;
        xzSpeed = dirToDestination.normalized * distToDestination * gravity / vSpeed;
    }
}
