using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererPositionStealer : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {
        var lineRenderer = GetComponent<LineRenderer>();
        positions = new Vector3[lineRenderer.positionCount];
        for(int i =0; i<positions.Length;i++)
        {
            Vector3 position = lineRenderer.GetPosition(i);
            positions[i] = new Vector3(position.x,1, position.z);
        }
        enemySpawner.positions = positions;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
