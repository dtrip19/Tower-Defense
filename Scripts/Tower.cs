using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    Enemy target;
    int timer = 50;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if (timer >= 100 && target != null)
        {
            timer = 0;
            var bulletRb = Instantiate(bullet).GetComponent<Rigidbody>();
            bulletRb.transform.position = transform.position;
            var directionEnemy = target.Transform.position - transform.position;
            bulletRb.velocity = directionEnemy.normalized * bulletSpeed;
            Destroy(bulletRb.gameObject, 15);
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
        }
    }
}
