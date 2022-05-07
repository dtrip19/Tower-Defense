using UnityEngine;
using System;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform modelRootTransform;

    public Vector3[] positions;
    public List<EnemyAttribute> attributes = new List<EnemyAttribute>();
    public bool canMove = true;
    public float moveSpeedMultiplier = 1;
    private EnemyScriptableObject enemySO;
    private Transform _transform;
    private int height;
    private int pathPositionIndex = 1;
    private int maxHealth;
    private int health;

    public float Speed
    {
        get
        {
            if (enemySO.attributes.Contains(EnemyAttribute.Persistent))
                return enemySO.moveSpeed / 10;
            else
                return (enemySO.moveSpeed + enemySO.moveSpeed * health / maxHealth) / 20;
        }
    }
    public Transform Transform => _transform;
    public int Health => health;
    public Vector3 LineOfSightPosition => _transform.position + new Vector3(0, 1, 0);
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
        if (!canMove) return;

        Vector3 newPos = new Vector3(positions[pathPositionIndex].x, positions[pathPositionIndex].y + height, positions[pathPositionIndex].z);
        Vector3 dirToPosition = newPos - _transform.position;
        Vector3 dirToMove = Speed * moveSpeedMultiplier * dirToPosition.normalized;
        _transform.position += dirToMove;
        modelRootTransform.forward = dirToMove;
        if (dirToMove.magnitude >= dirToPosition.magnitude - 0.05f)
        {
            pathPositionIndex++;
            if (pathPositionIndex > positions.Length - 1)
            {
                OnReachEndPath?.Invoke(health);
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int damage, DamageType damageType)
    {
        float damageToTake = damage;
        if (attributes.Contains(EnemyAttribute.Armored) && damageType != DamageType.Piercing && damageType != DamageType.Explosive)
            damageToTake /= 2;
        if (attributes.Contains(EnemyAttribute.Resistant) && damageType != DamageType.Elemental)
            damageToTake /= 2;

        health -= (int)damageToTake;
        if (health <= 0)
        {
            enemySO.deathLocation = new Vector3(_transform.position.x, _transform.position.y + enemySO.height, transform.position.z);
            OnDeath?.Invoke(enemySO);
            Destroy(gameObject);
        }
    }

    public void ReduceHeight(int value)
    {
        height -= value;
        if (height < 0)
            height = 0;
    }

    public void InitEnemy(EnemyScriptableObject enemySO)
    {
        this.enemySO = enemySO;
        health = maxHealth = enemySO.health;
        height = enemySO.height;
        foreach (var attribute in enemySO.attributes)
        {
            attributes.Add(attribute);
        }
        GetComponentInChildren<MeshRenderer>().material = enemySO.material;
        OnSpawn?.Invoke(this);
    }
}