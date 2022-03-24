using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    public Vector3[] positions;
    Transform _transform;
    int pathPositionIndex = 0;
    int maxHealth;
    int health;
    public float Speed => .25f+health/8;
    public Transform Transform => _transform;
    public static event Action<int> OnReachEndPath;
    // Start is called before the first frame update
    void Awake()
    {
        _transform = GetComponent<Transform>();
        
    }

    private void FixedUpdate()
    {
        Vector3 dirToPosition = (positions[pathPositionIndex] - transform.position);
        Vector3 dirToMove = dirToPosition.normalized * Speed;
        _transform.position += dirToMove;
        if (dirToMove.magnitude >= dirToPosition.magnitude){
            pathPositionIndex++;
            if (pathPositionIndex > positions.Length - 1){
                OnReachEndPath?.Invoke(health);
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void setHealth(int health){
        this.health = health;
        this.maxHealth = health;
    }
}
