using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public List<EnemyAttribute> attributes;
    public int health;
    public float moveSpeed;

    public int points;

    public Vector3 deathLocation; //set by enemy on death
}
