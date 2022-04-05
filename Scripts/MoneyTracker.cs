using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTracker : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    int points = 200;

    // Start is called before the first frame update
    void Start()
    {
        TowerSlot.OnSelect += SpendPoints;
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = points.ToString();
    }

    void SpendPoints(TowerScriptableObject towerScriptableObject)
    {
        points -= towerScriptableObject.price;
        textMesh.text = points.ToString();
    }
}
