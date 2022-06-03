using System.Collections;
using UnityEngine;

public class ParasiteEnemy : MonoBehaviour
{
    private GameObject rangeIndicator;
    private Transform _transform;
    private Enemy enemy;
    private TowerBehaviorBase hostTower;
    private const int range = 8;
    private bool isAttached;

    private void Awake()
    {
        rangeIndicator = Resources.Load<GameObject>("Enemies/ParasiteRangeIndicator");
        _transform = transform;
        Instantiate(rangeIndicator, _transform);
        enemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        if (isAttached) return;

        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Tower);
        Transform closestTowerTransform = null;
        TowerBehaviorBase closest = null;
        float shortestDistance = float.MaxValue;
        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out TowerBehaviorBase behavior) || !behavior.canShoot || behavior.ammo == 0) continue;

            var towerTransform = behavior.transform;
            var distance = Vector3.Distance(_transform.position, towerTransform.position);
            if (distance < shortestDistance)
            {
                closestTowerTransform = towerTransform;
                shortestDistance = distance;
                closest = behavior;
            }
        }

        if (closest != null)
            StartCoroutine(SuckTargetCoroutine(closest, closestTowerTransform));
    }

    private IEnumerator SuckTargetCoroutine(TowerBehaviorBase behavior, Transform towerTransform)
    {
        isAttached = true;
        hostTower = behavior;
        enemy.canMove = false;

        while (Vector3.Distance(_transform.position, towerTransform.position) > 0.1f)
        {
            _transform.position = Vector3.Lerp(_transform.position, towerTransform.position, Time.deltaTime * 8);

            yield return null;
        }

        _transform.position = towerTransform.position + new Vector3(0, behavior.bulletOriginHeight, 0);
        behavior.canShoot = false;

        while (behavior.ammo > 0)
        {
            behavior.ammo -= Mathf.Clamp(behavior.maxAmmo / 10, 0, behavior.ammo);
            enemy.Heal(100);

            yield return new WaitForSeconds(1);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (hostTower != null)
            hostTower.canShoot = true;
    }
}
