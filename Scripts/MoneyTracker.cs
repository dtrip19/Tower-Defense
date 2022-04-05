using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTracker : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    public static int Points {get; private set;} = 200;
    // Start is called before the first frame update
    void Start()
    {
        TowerPlacementManager.OnPlace += SpendPoints;
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = Points.ToString();
    }

    void SpendPoints(TowerScriptableObject towerScriptableObject)
    {
        Points -= towerScriptableObject.price;
        textMesh.text = Points.ToString();
    }
}
