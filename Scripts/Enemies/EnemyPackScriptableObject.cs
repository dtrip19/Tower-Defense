using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Pack")]
public class EnemyPackSciptableObject : ScriptableObject
{
    public string enemyFilePath;
    public int numEnemies;
    public float spawnStartTime;
    public float spawnInterval;
}
