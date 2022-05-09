using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehavior : TowerBehaviorBase
{
    protected override void FixedUpdate()
    {
        timer++;
        if (timer >= attackDelay && target != null)
        {
            timer = 0;
            Shoot();
        }
    }

    protected override void Shoot()
    {
        transform.forward = target.transform.position - transform.position;

        // Destroy(gameObject);
    }
}
