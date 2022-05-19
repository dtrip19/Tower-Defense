using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerScriptableObject towerSO;
    private Describable describable;
    private Transform _transform;

    public static event Action<Tower> OnSelect;

    private void Awake()
    {
        describable = GetComponent<Describable>();
        _transform = transform;
    }

    public void Upgrade(int upgradeIndex)
    {
        SetScriptableObject(towerSO.upgrades[upgradeIndex]);
        OnSelect?.Invoke(this);
    }

    public void SetScriptableObject(TowerScriptableObject towerSO)
    {
        this.towerSO = towerSO;
        var collider = _transform.GetChild(0);
        collider.GetComponent<SphereCollider>().radius = towerSO.colliderSize;

        foreach (Transform child in _transform)
        {
            if (child.gameObject.tag == "TowerModel")
                Destroy(child.gameObject);
        }
        if (towerSO.model != null)
            Instantiate(towerSO.model, _transform);
        else
            Instantiate(Resources.Load<GameObject>("Towers/DefaultTowerModel"), _transform);
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
            case 12:
                behavior = gameObject.AddComponent<BounceTowerBehavior>();
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
            case 23:
                behavior = gameObject.AddComponent<StumpTowerBehavior>();
                break;
            case 24:
                behavior = gameObject.AddComponent<WatermelonTowerBehavior>();
                break;
            case 25:
                behavior = gameObject.AddComponent<TreeTowerBehavior>();
                break;
            case 26:
                behavior = gameObject.AddComponent<RootTowerBehavior>();
                break;
        }

        var towerData = new TowerData
        {
            attackDelay = towerSO.attackDelay,
            damage = towerSO.damage,
            bulletSpeed = towerSO.bulletSpeed,
            range = towerSO.range,
            lifeTime = towerSO.lifeTime,
            bullet = towerSO.bullet,
            bulletOriginHeight = towerSO.bulletOriginHeight,
            pierce = towerSO.pierce,
            ammo = towerSO.ammo,
        };
        behavior.SetTowerInfo(towerData);

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
