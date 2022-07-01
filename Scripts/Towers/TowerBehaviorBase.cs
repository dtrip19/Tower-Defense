using UnityEngine;
using System;

public abstract class TowerBehaviorBase : MonoBehaviour
{
    public GameObject bullet;
    public GameObject model;
    public Transform towerHeadTransform;
    public float range;
    public int attackDelay;
    public int damage;
    public int pierce;
    public int ammo;
    public int maxAmmo;
    public int towerID;
    public float bulletSpeed;
    public float lifeTime;
    public float bulletOriginHeight;
    public bool canShoot;
    protected Transform _transform;
    protected Enemy target;
    protected int timer;

    public virtual Vector3 BulletOrigin => new Vector3(_transform.position.x, _transform.position.y + bulletOriginHeight, _transform.position.z);
    protected virtual DamageType DamageType => DamageType.Normal;
    protected virtual ProjectileType ProjectileType => ProjectileType.Normal;
    public static event Action<TowerBehaviorBase> OnSpawn;

    protected virtual void Awake()
    {
        _transform = transform;
    }

    protected void Update()
    {
        if (target != null && !target.Equals(null))
        {
            FaceTarget();
            if (Vector3.Distance(target.Transform.position, _transform.position) > range || !IsTargetVisible(target))
                target = null;
            else
                return;
        }

        target = FindFurthestTarget();
    }

    public void FaceTarget(){
        if(towerHeadTransform != null && ammo > 0){
            towerHeadTransform.LookAt(target.transform);
        }
    }

    public void SetTowerInfo(TowerData towerData)
    {
        attackDelay = towerData.attackDelay;
        damage = towerData.damage;
        bulletSpeed = towerData.bulletSpeed;
        range = towerData.range;
        lifeTime = towerData.lifeTime;
        bullet = towerData.bullet;
        bulletOriginHeight = towerData.bulletOriginHeight;
        pierce = towerData.pierce;
        maxAmmo = towerData.ammo;
        ammo = maxAmmo;
        towerID = towerData.towerID;
        if (maxAmmo != 0)
            OnSpawn?.Invoke(this);
    }

    public virtual Enemy FindFurthestTarget()
    {
        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);

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
        return newTarget;
    }

    protected void FixedUpdate()
    {
        timer++;
        if (ammo > 0 && timer >= attackDelay && target != null && canShoot)
        {
            timer = 0;
            Shoot();
            ammo--;
        }
    }

    protected virtual void Shoot()
    {
        var projectile = Instantiate(bullet).GetComponent<Projectile>();
        projectile.Transform.position = BulletOrigin;
        var dirToEnemy = target.LineOfSightPosition - BulletOrigin;

        projectile.SetValues(dirToEnemy.normalized, DamageType, Time.time + lifeTime, bulletSpeed, damage, pierce);
        projectile.projectileType = ProjectileType;
    }

    public virtual bool IsTargetVisible(Enemy enemy)
    {
        var lineOfSightPosition = new Vector3(_transform.position.x, _transform.position.y + bulletOriginHeight, _transform.position.z);
        Vector3 dirToTarget = (enemy.LineOfSightPosition - lineOfSightPosition).normalized;
        return !Physics.Raycast(lineOfSightPosition, dirToTarget, out RaycastHit hit, range, Layers.Unplaceable);
    }
}
