using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform modelRootTransform;

    public EnemyScriptableObject enemyScriptableObject;
    public Vector3[] positions;
    private Transform _transform;
    private int pathPositionIndex = 1;
    private int maxHealth;
    private int health;

    public float Speed
    {
        get
        {
            if (enemyScriptableObject.attributes.Contains(EnemyAttribute.Persistent))
                return enemyScriptableObject.moveSpeed;
            else
                return enemyScriptableObject.moveSpeed * health / maxHealth;
        }
    }
    public Transform Transform => _transform;
    public int Health => health;
    public Vector3 LineOfSightPosition => new Vector3(_transform.position.x, _transform.position.y + enemyScriptableObject.height, _transform.position.z);
    public int PathPositionIndex => pathPositionIndex;

    public static event Action<int> OnReachEndPath;
    public static event Action<Enemy> OnSpawn;

    public static event Action<EnemyScriptableObject> OnDeath;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {    
        Vector3 dirToPosition = positions[pathPositionIndex] - _transform.position;
        Vector3 dirToMove = dirToPosition.normalized * Speed;
        _transform.position += dirToMove;
        modelRootTransform.forward = dirToMove.normalized;
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
            enemyScriptableObject.deathLocation = new Vector3(_transform.position.x,_transform.position.y + enemyScriptableObject.height, transform.position.z);
            OnDeath?.Invoke(enemyScriptableObject);
            Destroy(gameObject);
        }
    }

    public void SetHealth(int health)
    {
        this.health = health;
        maxHealth = health;
        OnSpawn?.Invoke(this);
    }
}
