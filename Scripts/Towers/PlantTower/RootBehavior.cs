using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehavior : TowerBehaviorBase
{
    new protected void Awake()
    {
        base.Awake();

        range = 8;
        attackDelay = 300;
        _transform.eulerAngles = new Vector3(-90, 0, 0);
    }

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
        var dirToEnemy = target.transform.position - transform.position;
        transform.forward = new Vector3(dirToEnemy.x, 0, dirToEnemy.z);
        GetComponent<Animator>().Play("Swing");

        Destroy(gameObject, 0.5f);
    }

    private void OnDestroy()
    {
        var point0 = _transform.position + _transform.forward.normalized * 0.5f;
        var point1 = _transform.position + _transform.forward.normalized * 7.5f;
        var colliders = Physics.OverlapCapsule(point0, point1, 0.5f, Layers.Enemy);

        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out Enemy enemy)) continue;

            enemy.TakeDamage(damage, DamageType.Normal);
        }
    }
}
