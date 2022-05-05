using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerScriptableObject towerSO;
    private Describable describable;

    public static event Action<Tower> OnSelect;

    private void Awake()
    {
        describable = GetComponent<Describable>();
    }

    public void Upgrade(int upgradeIndex)
    {
        SetScriptableObject(towerSO.upgrades[upgradeIndex]);
        OnSelect?.Invoke(this);
    }

    public void SetScriptableObject(TowerScriptableObject towerSO)
    {
        this.towerSO = towerSO;
        var child = transform.GetChild(0);
        child.GetComponent<SphereCollider>().radius = towerSO.colliderSize;
        AttachBehaviorComponent();
    }

    public void AttachBehaviorComponent()
    {
        bool hasBehavior = TryGetComponent(out TowerBehaviorBase behavior);
        if (hasBehavior)
            Destroy(behavior);

        switch (towerSO.towerId)
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
                behavior = gameObject.AddComponent<HeavyTowerBehavior>();
                break;
            case 10:
                behavior = gameObject.AddComponent<TornadoTowerBehavior>();
                break;
            case 11:
                behavior = gameObject.AddComponent<VolcanoTowerBehavior>();
                break;
            case 13:
                behavior = gameObject.AddComponent<HurricaneTowerBehavior>();
                break;
            case 14:
                behavior = gameObject.AddComponent<GeyserTowerBehavior>();
                break;
            case 15:
                behavior = gameObject.AddComponent<MagicTowerBehavior>();
                break;
            case 16:
                behavior = gameObject.AddComponent<TeslaTowerBehavior>();
                break;
            case 17:
                behavior = gameObject.AddComponent<RainCloudTowerBehavior>();
                break;
            case 18:
                behavior = gameObject.AddComponent<LightningWaveTowerBehavior>();
                break;
            case 19:
                behavior = gameObject.AddComponent<ChainLightningTowerBehavior>();
                break;
            case 20:
                behavior = gameObject.AddComponent<AcidRainTowerBehavior>();
                break;
            case 21:
                behavior = gameObject.AddComponent<GravityTowerBehavior>();
                break;
            case 22:
                behavior = gameObject.AddComponent<UtilityTowerBehavior>();
                break;

        }
        behavior.attackDelay = towerSO.attackDelay;
        behavior.damage = towerSO.damage;
        behavior.bulletSpeed = towerSO.bulletSpeed;
        behavior.range = towerSO.range;
        behavior.lifeTime = towerSO.lifeTime;
        behavior.bullet = towerSO.bullet;
        behavior.bulletOriginHeight = towerSO.bulletOriginHeight;
        behavior.pierce = towerSO.pierce;

        if (hasBehavior)
            behavior.canShoot = true;
    }

    private void OnMouseEnter()
    {
        UI.cursorEnter(towerSO, describable);
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
