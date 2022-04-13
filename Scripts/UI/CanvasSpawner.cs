using UnityEngine;
using TMPro;

public class CanvasSpawner : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] Transform clonesTransform;
    [SerializeField] GameObject healthBarObject;

    [SerializeField] GameObject pointNumbers;

    private void Awake()
    {
        Enemy.OnSpawn += SpawnHealthBar;
        Enemy.OnDeath += SpawnPointNumbers;
    }

    private void SpawnHealthBar(Enemy enemy)
    {
        var healthBar = Instantiate(healthBarObject, clonesTransform).GetComponent<HealthBar>();
        healthBar.SetFollowTarget(enemy, mainCam);
    }

    void SpawnPointNumbers(EnemyScriptableObject enemyScriptableObject)
    {
        var spawnLocation = mainCam.WorldToScreenPoint(enemyScriptableObject.deathLocation);
        var pointNumbersTransform = (RectTransform)Instantiate(pointNumbers, clonesTransform).transform;
        pointNumbersTransform.GetComponent<TextMeshProUGUI>().text = "+" + enemyScriptableObject.points.ToString();
        pointNumbersTransform.anchoredPosition = new Vector2(spawnLocation.x - Screen.width/2 +70,spawnLocation.y - Screen.height/2);
        Destroy(pointNumbersTransform.gameObject, 2);
    }
}
