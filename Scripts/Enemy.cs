using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    public Vector3[] positions;
    int health;
    
    Transform _transform;

    public Transform Transform => _transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        
    }

    private void FixedUpdate()
    {
        _transform.position += new Vector3(speed, 0, 0);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
