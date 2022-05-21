using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public GameObject modelPrefab;
    public Material material;
    public List<EnemyAttribute> attributes;
    public CapsuleDirection capsuleDirection;
    public float capsuleRadius;
    public float capsuleHeight;
    public int health;
    public float moveSpeed;
    public int points;
    public int height = 1;

    public Vector3 deathLocation; //set by enemy on death
}

public enum CapsuleDirection { X, Y, Z }
