using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public Material material;
    public List<EnemyAttribute> attributes;
    public int health;
    public float moveSpeed;
    public int points;
    public int height = 1;

    public Vector3 deathLocation; //set by enemy on death
}
