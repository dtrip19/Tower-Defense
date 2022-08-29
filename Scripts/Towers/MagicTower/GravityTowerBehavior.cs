using UnityEngine;

public class GravityTowerBehavior : RainCloudTowerBehavior
{
    private GameObject gravityIndicatorObject;

    new private void Awake()
    {
        base.Awake();

        gravityIndicatorObject = Resources.Load<GameObject>("Towers/MagicTower/GravityTowerIndicator");
    }

    protected override void Shoot()
    {
        var cloudPosition = rainCloudTransform.position;
        var point0 = new Vector3(cloudPosition.x, 1, cloudPosition.z);
        var point1 = new Vector3(cloudPosition.x, 15, cloudPosition.z);
        var colliders = Physics.OverlapCapsule(point0, point1, 1);

        var indicator = Instantiate(gravityIndicatorObject);
        indicator.transform.position = new Vector3(cloudPosition.x, 7.5f, cloudPosition.z);
        Destroy(indicator, 0.5f);

        float heightCollidedUnplaceableObject = -100;
        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == Layers.UnplaceableRaw)
            {
                if (Physics.Raycast(rainCloudTransform.position, Vector3.down, out var hitInfo, 50, Layers.Unplaceable) && hitInfo.point.y > heightCollidedUnplaceableObject)
                    heightCollidedUnplaceableObject = hitInfo.point.y;
            }

            int enemiesHit = 0;
            if (!collider.TryGetComponent(out Enemy enemy) || enemy.Transform.position.y < heightCollidedUnplaceableObject) continue;

            enemy.TakeDamage(damage, DamageType.Elemental);
            enemy.ReduceHeight(2);

            if (enemiesHit++ > pierce)
                return;
        }
    }
}
