using UnityEngine;
using TMPro;

public class PointTracker : MonoBehaviour
{
    [SerializeField] bool usePoints;

    private TextMeshProUGUI textMesh;

    public static int Points { get; private set; } = 125;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Points += 100;
            textMesh.text = Points.ToString();
        }
    }
    
    private void Awake()
    {
        TowerPlacementManager.OnPlace += SpendPoints;
        TowerUpgradeButton.OnUpgrade += SpendPoints;
        Enemy.OnDeath += GainPoints;
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = Points.ToString();
    }

    private void SpendPoints(TowerScriptableObject towerScriptableObject)
    {
        if (!usePoints) return;

        Points -= towerScriptableObject.price;
        textMesh.text = Points.ToString();
    }

    private void GainPoints(EnemyScriptableObject enemyScriptableObject)
    {
        Points += enemyScriptableObject.points;
        textMesh.text = Points.ToString();
    }

    private void OnDestroy()
    {
        TowerPlacementManager.OnPlace -= SpendPoints;
        TowerUpgradeButton.OnUpgrade -= SpendPoints;
    }
}
