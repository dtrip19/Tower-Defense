using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonTowerProjectile : Projectile
{

    private Vector3 destination;
    private float distToDestination;

    private float gravity = -.4f;
    private float vSpeed = 2;


    new private void FixedUpdate()
    {
        base.FixedUpdate();
        vSpeed += gravity;
        _transform.position += new Vector3(0,vSpeed,0);
    }
    public override void SetValues(Vector3 destination, DamageType damageType, float timeDestroy, float speed, int damage, int pierce)
    {
        this.destination = destination;
        this.damageType = damageType;
        this.timeDestroy = timeDestroy;
        this.damage = damage;
        this.pierce = pierce;
        this.distToDestination = Vector3.Distance(_transform.position, destination);
        var dirToDestination = (destination - _transform.position).normalized;
        direction = new Vector3(dirToDestination.x, 0, dirToDestination.z);
        this.speed = distToDestination * speed /2;
        // vSpeed = destination.y/10;

        vSpeed =  speed + destination.y/10;
        gravity = -speed * speed * .1f;
    }
}
