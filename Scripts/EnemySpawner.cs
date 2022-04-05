using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;

    public Vector3[] positions;
    private int timer = 0;

    private void FixedUpdate()
    {
        timer++;
        if (timer >= 100)
        {
            timer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(enemyObject).GetComponent<Enemy>();
        enemy.Transform.position = positions[0];
        enemy.GetComponent<Collider>().isTrigger = false;
        enemy.positions = positions;
        enemy.SetHealth(Random.Range(1,10));
    }
}
