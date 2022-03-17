using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeTime;
    Enemy target;
    int timer = 30;
    int bulletDamage;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if (timer >= 30 && target != null)
        {
            timer = 0;
            Shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy) && target == null)
        {
            target = enemy;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy) && target != null && target.Equals(enemy))
        {
            target = null;

            var colliders = Physics.OverlapSphere(transform.position, 9.84f);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out enemy))
                {
                    target = enemy;
                }
            }
        }
    }

    void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.Transform.position = transform.position;
        var directionEnemy = target.Transform.position - transform.position;

        projectile.direction = directionEnemy.normalized;
        projectile.speed = bulletSpeed;
        projectile.damage = bulletDamage;
        Destroy(projectile.gameObject, bulletLifeTime);
    }
}
