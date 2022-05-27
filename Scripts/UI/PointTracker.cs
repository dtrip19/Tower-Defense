using UnityEngine;
using TMPro;

public class PointTracker : MonoBehaviour
{
    [SerializeField] bool usePoints;

    private TextMeshProUGUI textMesh;

    public static int Points { get; private set; }

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
        Tower.OnRefillAmmo += SpendPoints;
        Enemy.OnDeath += GainPoints;
        EnemySpawner.OnSkipWave += GainAmountPoints;
        textMesh = GetComponent<TextMeshProUGUI>();
        Points = 125;
        textMesh.text = Points.ToString();
    }

    private void SpendPoints(int pointsToSpend)
    {
        if (!usePoints) return;

        Points -= pointsToSpend;
        textMesh.text = Points.ToString();
    }

    private void GainPoints(EnemyScriptableObject enemyScriptableObject)
    {
        Points += enemyScriptableObject.points;
        textMesh.text = Points.ToString();
    }

    private void GainAmountPoints(int amount)
    {
        Points += amount;
        textMesh.text = Points.ToString();
    }

    private void OnDestroy()
    {
        TowerPlacementManager.OnPlace -= SpendPoints;
        TowerUpgradeButton.OnUpgrade -= SpendPoints;
        Tower.OnRefillAmmo -= SpendPoints;
        Enemy.OnDeath -= GainPoints;
        EnemySpawner.OnSkipWave -= GainAmountPoints;
    }
}
