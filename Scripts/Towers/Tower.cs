using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerScriptableObject towerScriptableObject;
    private Describable describable;

    public static event Action<Tower> OnSelect;

    private void Awake()
    {
        describable = GetComponent<Describable>();
    }

    public void Upgrade(int upgradeIndex)
    {
        SetScriptableObject(towerScriptableObject.upgrades[upgradeIndex]);
        OnSelect?.Invoke(this);
    }

    public void SetScriptableObject(TowerScriptableObject towerScriptableObject)
    {
        this.towerScriptableObject = towerScriptableObject;
        var child = transform.GetChild(0);
        child.GetComponent<SphereCollider>().radius = towerScriptableObject.colliderSize;
        child.localPosition = new Vector3(0, towerScriptableObject.colliderSize / 2, 0);
        AttachBehaviorComponent();
    }

    public void AttachBehaviorComponent()
    {
        bool hasBehavior = TryGetComponent(out TowerBehaviorBase behavior);
        if (hasBehavior)
            Destroy(behavior);

        switch (towerScriptableObject.towerId)
        {
            case 1: 
                behavior = gameObject.AddComponent<BasicTowerBehavior>();
                break;
            case 2:
                behavior = gameObject.AddComponent<FastTowerBehavior>();
                break;
            case 3:
                behavior = gameObject.AddComponent<SlowTowerBehavior>();
                break;
            case 4:
                behavior = gameObject.AddComponent<DragonTowerBehavior>();
                break;
            case 5:
                behavior = gameObject.AddComponent<InceptionTowerBehavior>();
                break;
            case 6:
                behavior = gameObject.AddComponent<RailgunTowerBehavior>();
                break;
            case 7:
                behavior = gameObject.AddComponent<OncePunchTowerBehavior>();
                break;
            case 8:
                behavior = gameObject.AddComponent<RandomTowerBehavior>();
                break;
            case 9:
                behavior = gameObject.AddComponent<MagicTowerBehavior>();
                break;
            case 10:
                behavior = gameObject.AddComponent<UtilityTowerBehavior>();
                break;

        }
        behavior.attackDelay = towerScriptableObject.attackDelay;
        behavior.damage = towerScriptableObject.damage;
        behavior.bulletSpeed = towerScriptableObject.bulletSpeed;
        behavior.range = towerScriptableObject.range;
        behavior.lifeTime = towerScriptableObject.lifeTime;
        behavior.bullet = towerScriptableObject.bullet;
        behavior.bulletOriginHeight = towerScriptableObject.bulletOriginHeight;
        behavior.pierce = towerScriptableObject.pierce;

        if (hasBehavior)
            behavior.canShoot = true;
    }

    private void OnMouseEnter()
    {
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
