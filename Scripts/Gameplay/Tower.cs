using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerScriptableObject towerScriptableObject;
    private Enemy target;
    private Describable describable;
    private Transform _transform;
    public bool canShoot = false;
    private int timer;
    private int enemyLayer = 1 << 11;
    private int unplaceableLayer = 1 << 9;

    public Vector3 BulletOrigin
    {
        get
        {
            var offset = towerScriptableObject.bulletOriginHeight;
            return new Vector3(_transform.position.x, _transform.position.y + offset, _transform.position.z);
        }
    }

    public static event Action<Tower> OnSelect;

    private void Awake()
    {
        describable = GetComponent<Describable>();
        _transform = transform;
    }

    private void FixedUpdate()
    {
        timer++;
        if (timer >= towerScriptableObject.attackDelay && target != null && canShoot)
        {
            timer = 0;
            Shoot();
        }
    }

    private void Update()
    {
        if (target != null && !target.Equals(null))
        {
            if (Vector3.Distance(target.Transform.position, _transform.position) > towerScriptableObject.range || !IsTargetVisible(target))
                target = null;
            else
                return;
        }

        var colliders = Physics.OverlapSphere(_transform.position, towerScriptableObject.range, enemyLayer);

        Enemy newTarget = null;
        int furthestIndex = 0;
        foreach (var collider in colliders)
        {
            if (!collider.TryGetComponent(out Enemy enemy)) continue;

            int pathPositionIndex = enemy.PathPositionIndex;
            if (pathPositionIndex > furthestIndex && IsTargetVisible(enemy))
            {
                furthestIndex = pathPositionIndex;
                newTarget = enemy;
            }
        }
        target = newTarget;
    }

    private void Shoot()
    {
        var projectile = Instantiate(towerScriptableObject.bullet).GetComponent<Projectile>();
        projectile.Transform.position = BulletOrigin;
        var dirToEnemy = target.LineOfSightPosition - BulletOrigin;

        projectile.direction = dirToEnemy.normalized;
        projectile.speed = towerScriptableObject.bulletSpeed;
        projectile.damage = towerScriptableObject.damage;
        Destroy(projectile.gameObject, towerScriptableObject.lifeTime / 10);
    }

    public void Upgrade(int upgradeIndex)
    {
        SetScriptableObject(towerScriptableObject.upgrades[upgradeIndex]);
    }

    public void SetScriptableObject(TowerScriptableObject towerScriptableObject)
    {
        this.towerScriptableObject = towerScriptableObject;
        var child = transform.GetChild(0);
        child.GetComponent<SphereCollider>().radius = towerScriptableObject.colliderSize;
        child.localPosition = new Vector3(0, towerScriptableObject.colliderSize / 2, 0);
    }

    private bool IsTargetVisible(Enemy enemy)
    {
        Vector3 bulletOrigin = new Vector3(_transform.position.x, _transform.position.y + towerScriptableObject.bulletOriginHeight, _transform.position.z);
        Vector3 dirToTarget = (enemy.LineOfSightPosition - bulletOrigin).normalized;
        return !Physics.Raycast(bulletOrigin, dirToTarget, out RaycastHit hit, towerScriptableObject.range, unplaceableLayer);
    }

    private void OnMouseEnter()
    {
        print("Moused over tower");
        var towerData = new TowerData
        {
            bulletSpeed = towerScriptableObject.bulletSpeed,
            damage = towerScriptableObject.damage,
            lifeTime = towerScriptableObject.lifeTime,
            attackDelay = towerScriptableObject.attackDelay,
            description = towerScriptableObject.description
        };

        describable.Inspect(towerData);
    }

    private void OnMouseExit()
    {
        describable.Uninspect();
    }

    private void OnMouseDown()
    {
        OnSelect?.Invoke(this);
    }
}
