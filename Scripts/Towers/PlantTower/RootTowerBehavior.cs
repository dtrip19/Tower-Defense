using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTowerBehavior : TowerBehaviorBase
{
    private GameObject rootPrefab;

    new private void Awake()
    {
        base.Awake();
        rootPrefab = Resources.Load<GameObject>("Towers/PlantTower/Root");
    }

    protected override void Shoot()
    {
        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy) && enemy.Height < 0)
            {
                enemy.TakeDamage(damage, DamageType.Normal);
            }
        }
        SpawnRoot();
    }

    private void SpawnRoot()
    {
        var root = Instantiate(rootPrefab);
        var randomPosition = Random.insideUnitCircle * range;
        root.transform.position = new Vector3(transform.position.x + randomPosition.x, 0, transform.position.z + randomPosition.y);
        //root.transform.forward = new Vector3(270,0,0);
    }
}
