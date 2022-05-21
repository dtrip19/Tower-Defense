using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMelonTowerProjectile : WatermelonTowerProjectile
{

    new protected void OnCollisionEnter(Collision collision)
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
                    enemy.moveSpeedMultiplier = .6f;
                }
            }
            Destroy(gameObject);
        }
    }    
}
