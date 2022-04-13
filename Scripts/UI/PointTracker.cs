using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointTracker : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    public static int Points {get; private set;} = 200;
    // Start is called before the first frame update
    void Start()
    {
        TowerPlacementManager.OnPlace += SpendPoints;
        Enemy.OnDeath += GainPoints;
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = Points.ToString();
    }

    void SpendPoints(TowerScriptableObject towerScriptableObject)
    {
        Points -= towerScriptableObject.price;
        textMesh.text = Points.ToString();
    }

    void GainPoints(EnemyScriptableObject enemyScriptableObject)
    {
        Points += enemyScriptableObject.points;
        textMesh.text = Points.ToString();
    }
}
