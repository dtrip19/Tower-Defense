using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave")]
public class EnemyWaveScriptableObjects : ScriptableObject
{
    public List<EnemyPackSciptableObject> enemyPacks;
}
