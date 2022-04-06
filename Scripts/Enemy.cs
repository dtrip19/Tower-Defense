using UnityEngine;
using System;

public enum EnemyAttribute { Flying, Burrowing, Armored, Persistent }

public class Enemy : MonoBehaviour
{
    public Vector3[] positions;
    public EnemyScriptableObject enemyScriptableObject;
    private Transform _transform;
    private int pathPositionIndex = 1;
    private int maxHealth;
    private int health;

    public float Speed => 0.05f;// enemyScriptableObject.moveSpeed * health / maxHealth;
    public Transform Transform => _transform;
    public int PathPositionIndex => pathPositionIndex;

    public static event Action<int> OnReachEndPath;

    private void Awake()
    {
        _transform = GetComponent<Transform>();       
    }

    private void FixedUpdate()
    {       
        Vector3 dirToPosition = (positions[pathPositionIndex] - transform.position);
        Vector3 dirToMove = dirToPosition.normalized * Speed;
        _transform.position += dirToMove;
        if (dirToMove.magnitude >= dirToPosition.magnitude)
        {
            pathPositionIndex++;
            if (pathPositionIndex > positions.Length - 1)
            {
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

    public void SetHealth(int health)
    {
        this.health = health;
        maxHealth = health;
    }
}
