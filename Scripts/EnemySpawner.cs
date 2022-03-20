using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    int timer = 0;

    public Vector3[] positions;

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
        var enemyTransform = Instantiate(enemy).transform;
        enemyTransform.position = new Vector3(-20, 0.5f, 5);
    }
}
