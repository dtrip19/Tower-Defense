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
            positions[i] = lineRenderer.GetPosition(i);
        }
        enemySpawner.positions = positions;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
