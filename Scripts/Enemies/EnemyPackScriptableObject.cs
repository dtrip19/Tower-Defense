using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Pack")]
public class EnemyPackScriptableObject : ScriptableObject
{
    public EnemyScriptableObject enemySO;
    public int numEnemies;
    public float spawnStartTime;
    public float spawnInterval;
}
