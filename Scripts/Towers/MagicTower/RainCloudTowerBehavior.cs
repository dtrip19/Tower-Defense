using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloudTowerBehavior : TowerBehaviorBase
{
    Transform rainCloudTransform;
    const int rainCloudHeight = 15;
    float Variance => Random.Range(-1.5f,1.5f);
    new void Awake()
    {
        base.Awake();
        rainCloudTransform = Instantiate(Resources.Load<GameObject>("Towers/MagicTower/RainCloud")).GetComponent<Transform>();
        rainCloudTransform.position = new Vector3(_transform.position.x, rainCloudHeight, _transform.position.z);
    }
    new void FixedUpdate()
    {
        if(target != null && !target.Equals(null))
        {
            var cloudPosition = rainCloudTransform.position;
            var targetPosition = new Vector3(target.Transform.position.x,rainCloudHeight, target.Transform.position.z);
            rainCloudTransform.position = Vector3.Lerp(cloudPosition, targetPosition,.01f);
        }
        
        base.FixedUpdate();
    }

    protected override void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.Transform.position = new Vector3(rainCloudTransform.position.x + Variance, rainCloudTransform.position.y, rainCloudTransform.position.z + Variance);

        projectile.SetValues(Vector3.down, DamageType, Time.time + lifeTime, bulletSpeed, damage, pierce);
    }
}
