﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] TowerScriptableObject towerScriptableObject;
    Enemy target;
    int timer = 0;

    public bool canShoot = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if (timer >= towerScriptableObject.attackSpeed && target != null && canShoot)
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
        var projectile = Instantiate(towerScriptableObject.bullet).GetComponent<Projectile>();
        projectile.Transform.position = transform.position;
        var directionEnemy = target.Transform.position - transform.position;

        projectile.direction = directionEnemy.normalized;
        projectile.speed = towerScriptableObject.bulletSpeed;
        projectile.damage = towerScriptableObject.bulletDamage;
        Destroy(projectile.gameObject, towerScriptableObject.bulletLifeTime);
    }

    public void SetScriptableObject(TowerScriptableObject towerScriptableObject){
        this.towerScriptableObject = towerScriptableObject;
    }
}
