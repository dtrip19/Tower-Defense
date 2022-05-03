using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningWaveProjectile : Projectile
{

    public GameObject lightningBolt;
    int lightningTimer = 0;

    new void FixedUpdate()
    {
        base.FixedUpdate();
        lightningTimer++;
        if(lightningTimer >= 3)
        {
            CreateLightning();
            lightningTimer = 0;
        }
    }

    void CreateLightning()
    {
        var point0 = new Vector3(_transform.position.x,1,_transform.position.z);
        var point1 = new Vector3(_transform.position.x,100,_transform.position.z);
        var colliders = Physics.OverlapCapsule(point0,point1,1,Layers.Enemy);

        foreach(var collider in colliders)
        {
            if(collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage, damageType);
            }
        }

        var bolt = Instantiate(lightningBolt);
        // bolt.transform.position = new Vector3(_transform.position.x,100,_transform.position.z );
        
        bolt.transform.position = new Vector3(_transform.position.x + Random.Range(-.75f,.75f),100,_transform.position.z + Random.Range(-.75f,.75f));
        Destroy(bolt,0.2f);
    }

    public override void SetValues(Vector3 direction, DamageType damageType, float timeDestroy, float speed, int damage, int pierce)
    {
        base.SetValues(direction, damageType, timeDestroy, speed, damage, pierce);
        direction = new Vector3(direction.x,0, direction.z);
    }
}
