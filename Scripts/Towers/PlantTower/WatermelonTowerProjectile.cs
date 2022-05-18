using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonTowerProjectile : Projectile
{
    float gravity = .03f;
    float vSpeed;
    Vector3 xzSpeed;
    new private void FixedUpdate()
    {
        // _transform.position += direction;
        // direction = new Vector3(direction.x, direction.y - 30 * .002f, direction.z);
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

        // var vSpeed = 15 * distToDestination *speed;
        // vSpeed += (destination.y - _transform.position.y) * 15/vSpeed;
        // direction = new Vector3(dirToDestination.x, vSpeed, dirToDestination.z).normalized * speed;

        gravity *= speed * speed;
        vSpeed = speed;
        xzSpeed = dirToDestination.normalized * distToDestination * gravity / vSpeed;
        print('1');
        print(vSpeed);
        // vSpeed = ((destination.y - _transform.position.y) + distToDestination * distToDestination * gravity) / distToDestination;
        print(vSpeed);
    }
}
