using UnityEngine;

public class LineRendererPositionStealer : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject pathColliderObject;

    private Vector3[] positions;
    
    private void Start()
    {
        var lineRenderer = GetComponent<LineRenderer>();

        positions = new Vector3[lineRenderer.positionCount];
        for(int i =0; i<positions.Length;i++)
        {
            Vector3 position = lineRenderer.GetPosition(i);
            var newPosition = new Vector3(position.x, position.y + 0.05f, position.z);
            positions[i] = newPosition;

            var pathCollider = Instantiate(pathColliderObject);
            pathCollider.transform.position = newPosition;
        }
        lineRenderer.SetPositions(positions);
        enemySpawner.positions = positions;
    }
}
