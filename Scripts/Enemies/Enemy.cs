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
            float percentHealth = (float)health / maxHealth;
            if (enemySO.attributes.Contains(EnemyAttribute.Persistent))
                return enemySO.moveSpeed / 10;
            else
                return (enemySO.moveSpeed + enemySO.moveSpeed * percentHealth) / 20;
        }
    }
    public Transform Transform => _transform;
    public int Health => health;
    public int Height => height;
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
        if (attributes.Contains(EnemyAttribute.Armored) && (damageType == DamageType.Normal || damageType == DamageType.Explosive))
            damageToTake /= 2;
        if (attributes.Contains(EnemyAttribute.Resistant) && (damageType == DamageType.Elemental || damageType == DamageType.Explosive))
            damageToTake /= 2;

        health -= (int)damageToTake;
        if (health <= 0)
        {
            enemySO.deathLocation = new Vector3(_transform.position.x, _transform.position.y + enemySO.height, transform.position.z);
            OnDeath?.Invoke(enemySO);
            Destroy(gameObject);
        }
    }

    public void Heal(int healing)
    {
        health = Mathf.Min(health + healing, maxHealth);
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
        maxHealth = enemySO.health;
        health = enemySO.health;
        height = enemySO.height;
        foreach (var attribute in enemySO.attributes)
        {
            attributes.Add(attribute);
        }
        if (enemySO.modelPrefab != null)
            Instantiate(enemySO.modelPrefab, _transform);
        else
            Instantiate(Resources.Load<GameObject>("Enemies/EnemyCapsule"), _transform);
        modelRootTransform = _transform.GetChild(0);
        GetComponentInChildren<MeshRenderer>().material = enemySO.material;
        var capsule = GetComponent<CapsuleCollider>();
        capsule.direction = (int)enemySO.capsuleDirection;
        capsule.radius = enemySO.capsuleRadius;
        capsule.height = enemySO.capsuleHeight;
        if (capsule.direction == (int)CapsuleDirection.Y)
            capsule.center = new Vector3(0, Mathf.RoundToInt(capsule.height) / 2, 0);
        if (enemySO.attributes.Contains(EnemyAttribute.Healing))
            gameObject.AddComponent<HealingEnemy>();
        OnSpawn?.Invoke(this);
    }
}