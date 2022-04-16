﻿using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Pack")]
public class EnemyPackSciptableObject : ScriptableObject
{
    public EnemyScriptableObject enemySO;
    public int numEnemies;
    public float spawnStartTime;
    public float spawnInterval;
    public float timeLastSpawn;
    public bool finished;
}