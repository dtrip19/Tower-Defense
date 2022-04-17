using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave")]
public class EnemyWaveScriptableObject : ScriptableObject
{
    public List<EnemyPackScriptableObject> enemyPacks;
}
