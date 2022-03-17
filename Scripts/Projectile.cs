using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public int pierce = 1;
    public Vector3 direction;

    Transform _transform;
    public Transform Transform => _transform;


    // Start is called before the first frame update
    void Awake()
    {
        _transform = GetComponent<Transform>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _transform.position += direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            pierce--;
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
