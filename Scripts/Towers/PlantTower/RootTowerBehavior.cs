using UnityEngine;

public class RootTowerBehavior : TowerBehaviorBase
{
    private GameObject rootPrefab;

    new private void Awake()
    {
        base.Awake();
        rootPrefab = Resources.Load<GameObject>("Towers/PlantTower/Root");
    }

    new protected void FixedUpdate()
    {
        timer++;
        if (timer >= attackDelay && canShoot)
        {
            timer = 0;
            Shoot();
        }
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
        int iterations = 1;
        var root = Instantiate(rootPrefab);
        var randomOffset = Random.insideUnitCircle * range;
        var randomPosition = new Vector3(transform.position.x + randomOffset.x, 0, transform.position.z + randomOffset.y);
        while (LocationIsObstructed(randomPosition) && iterations++ < 50)
        {
            randomOffset = Random.insideUnitCircle * range;
            randomPosition = new Vector3(transform.position.x + randomOffset.x, 0, transform.position.z + randomOffset.y);
        }
        root.transform.position = randomPosition;
        root.GetComponent<RootBehavior>().damage = damage;
        root.GetComponent<RootBehavior>().lifeTime = lifeTime;
    }

    private bool LocationIsObstructed(Vector3 location)
    {
        var origin = new Vector3(location.x, 100, location.z);
        return Physics.SphereCast(origin, 1, Vector3.down, out RaycastHit hit, 120, Layers.MapCollider);
    }
}
