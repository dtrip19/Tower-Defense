using System.Collections.Generic;
using UnityEngine;

public class RootBehavior : TowerBehaviorBase
{
    private List<Enemy> enemiesHit = new List<Enemy>();
    private bool attacking;
    private float timeCreated;

    new protected void Awake()
    {
        base.Awake();
        timeCreated = Time.time;

        range = 8;
        attackDelay = 50;
        _transform.eulerAngles = new Vector3(-90, 0, 0);
    }

    new protected void FixedUpdate()
    {
        timer++;
        if (timer >= attackDelay && target != null)
        {
            timer = 0;
            Shoot();
        }

        if (Time.time > timeCreated + lifeTime)
            Destroy(gameObject);
    }

    protected override void Shoot()
    {
        attacking = true;
        var dirToEnemy = target.transform.position - transform.position;
        transform.forward = new Vector3(dirToEnemy.x, 0, dirToEnemy.z);
        GetComponent<Animator>().Play("Swing");

        Destroy(gameObject, 0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!attacking || !other.TryGetComponent(out Enemy enemy) || enemiesHit.Contains(enemy)) return;

        enemy.TakeDamage(damage, DamageType.Normal);
        enemiesHit.Add(enemy);
    }
}
