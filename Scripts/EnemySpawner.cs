using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    public Vector3[] positions;
    int timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if (timer >= 100)
        {
            timer = 0;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        var enemyComp = Instantiate(enemy).GetComponent<Enemy>();
        enemyComp.Transform.position = positions[0];
        enemyComp.GetComponent<Collider>().isTrigger = false;
        enemyComp.positions = positions;
        enemyComp.SetHealth(Random.Range(1,10));
    }
}
