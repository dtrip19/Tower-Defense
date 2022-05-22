using UnityEngine;

[CreateAssetMenu(menuName = "Map Spawns")]
public class MapSpawnsScriptableObject : ScriptableObject
{
    public EnemyWaveScriptableObject[] waves;
}
