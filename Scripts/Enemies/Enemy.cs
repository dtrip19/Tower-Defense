using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform modelRootTransform;

    private EnemyScriptableObject enemySO;
    public Vector3[] positions;
    private Transform _transform;
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
    public Vector3 LineOfSightPosition => _transform.position + new Vector3(0, 1, 0);// new Vector3(_transform.position.x, _transform.position.y + enemySO.height, _transform.position.z);
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
        Vector3 newPos = new Vector3(positions[pathPositionIndex].x, positions[pathPositionIndex].y + enemySO.height, positions[pathPositionIndex].z);
        Vector3 dirToPosition = newPos - _transform.position;
        Vector3 dirToMove = dirToPosition.normalized * Speed;
        _transform.position += dirToMove;
        modelRootTransform.forward = dirToMove.normalized;
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
        if (enemySO.attributes.Contains(EnemyAttribute.Armored) && damageType != DamageType.Piercing && damageType != DamageType.Explosive)
            damageToTake /= 2;
        if (enemySO.attributes.Contains(EnemyAttribute.Resistant) && damageType != DamageType.Elemental) return;

        health -= damage;
        if (health <= 0)
        {
            enemySO.deathLocation = new Vector3(_transform.position.x, _transform.position.y + enemySO.height, transform.position.z);
            OnDeath?.Invoke(enemySO);
            Destroy(gameObject);
        }
    }

    // public void SetHealth(int health)
    // {
    //     this.health = health;
    //     maxHealth = health;
    //     OnSpawn?.Invoke(this);
    // }

    public void InitEnemy(EnemyScriptableObject enemySO)
    {
        this.enemySO = enemySO;
        health = maxHealth = enemySO.health;
        GetComponentInChildren<MeshRenderer>().material = enemySO.material;
        OnSpawn?.Invoke(this);
    }
}