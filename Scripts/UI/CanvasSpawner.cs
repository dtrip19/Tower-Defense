using UnityEngine;

public class CanvasSpawner : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] Transform clonesTransform;
    [SerializeField] GameObject healthBarObject;

    private void Awake()
    {
        Enemy.OnSpawn += SpawnHealthBar;
    }

    private void SpawnHealthBar(Enemy enemy)
    {
        var healthBar = Instantiate(healthBarObject, clonesTransform).GetComponent<HealthBar>();
        healthBar.SetFollowTarget(enemy, mainCam);
    }
}
