using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptableObject : ScriptableObject
{
    public List<EnemyAttribute> attributes;
    public int health;
    public float moveSpeed;
}
