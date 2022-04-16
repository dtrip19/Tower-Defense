using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;

    public Vector3[] positions;
    private int timer;
    private int spawnIndex;

    private string[] wave1 = new string[10];

    private void Awake()
    {
        for(int i = 0; i <wave1.Length; i++)
        {
            wave1[i] = "BasicEnemy";
        }
        wave1[9] = "WeakEnemy";
    }
    private void FixedUpdate()
    {

        timer++;
        if (timer >= 100 && spawnIndex <= 9)
        {

            timer = 0;
            SpawnEnemy(wave1[spawnIndex++]);
        }
    }

    private void SpawnEnemy(string enemyFilePath)
    {
        var enemy = Instantiate(enemyObject).GetComponent<Enemy>();
        
        enemy.Transform.position = positions[0];
        enemy.GetComponent<Collider>().isTrigger = true;
        enemy.positions = positions;
        enemy.InitEnemy(Resources.Load<EnemyScriptableObject>(enemyFilePath));
    }
}
