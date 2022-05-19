using UnityEngine;
using TMPro;

public class CanvasSpawner : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] Transform clonesTransform;
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] GameObject pointNumbers;
    [SerializeField] GameObject ammoBarPrefab;

    private void Awake()
    {
        Enemy.OnSpawn += SpawnHealthBar;
        Enemy.OnDeath += SpawnPointNumbers;
        TowerBehaviorBase.OnSpawn += SpawnAmmoBar;
    }

    private void SpawnAmmoBar(TowerBehaviorBase behavior)
    {
        var ammoBar = Instantiate(ammoBarPrefab, clonesTransform).GetComponent<AmmoBar>();
        ammoBar.SetFollowTarget(behavior, mainCam);
    }

    private void SpawnHealthBar(Enemy enemy)
    {
        var healthBar = Instantiate(healthBarPrefab, clonesTransform).GetComponent<HealthBar>();
        healthBar.SetFollowTarget(enemy, mainCam);
    }

    private void SpawnPointNumbers(EnemyScriptableObject enemyScriptableObject)
    {
        var spawnLocation = mainCam.WorldToScreenPoint(enemyScriptableObject.deathLocation);
        var pointNumbersTransform = (RectTransform)Instantiate(pointNumbers, clonesTransform).transform;
        pointNumbersTransform.GetComponent<TextMeshProUGUI>().text = "+" + enemyScriptableObject.points.ToString();
        pointNumbersTransform.anchoredPosition = new Vector2(spawnLocation.x - Screen.width / 2 + 35, spawnLocation.y - Screen.height / 2);
        Destroy(pointNumbersTransform.gameObject, 2);
    }

    private void OnDestroy()
    {
        Enemy.OnSpawn -= SpawnHealthBar;
        Enemy.OnDeath -= SpawnPointNumbers;
        TowerBehaviorBase.OnSpawn -= SpawnAmmoBar;
    }
}
