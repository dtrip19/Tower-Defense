using UnityEngine;

public class KiteEnemy : MonoBehaviour
{
    private Enemy self;

    private void Awake()
    {
        self = GetComponent<Enemy>();

        self.InitEnemy(Resources.Load<EnemyScriptableObject>("Enemies/KiteEnemy"));
    }
}
