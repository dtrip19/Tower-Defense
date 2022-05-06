using UnityEngine;

public class VolcanoTowerProjectile : Projectile
{
    [SerializeField] GameObject explosionObject;
    [SerializeField] float explosionRadius;
    [SerializeField] int explosionDamage;

    new private void OnCollisionEnter(Collision collision)
    {
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
}
