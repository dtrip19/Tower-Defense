using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerScriptableObject towerScriptableObject;
    private Describable describable;
    private Transform _transform;
    private int timer;
    public static event Action<Tower> OnSelect;

    private void Awake()
    {
        describable = GetComponent<Describable>();
        _transform = transform;
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
        AttachBehaviorComponent();
    }

    public void AttachBehaviorComponent()
    {
        if (TryGetComponent(out TowerBehaviorBase behavior))
        {
            Destroy(behavior);

        }
        switch (towerScriptableObject.towerId)
        {
            case 1: 
                behavior = gameObject.AddComponent<BasicTowerBehavior>();
                break;
            case 2:
                behavior = gameObject.AddComponent<RandomTowerbehavior>();
                break;
        }
        behavior.attackDelay = towerScriptableObject.attackDelay;
        behavior.damage = towerScriptableObject.damage;
        behavior.bulletSpeed = towerScriptableObject.bulletSpeed;
        behavior.range = towerScriptableObject.range;
        behavior.lifeTime = towerScriptableObject.lifeTime;
        behavior.bullet = towerScriptableObject.bullet;
        behavior.bulletOriginHeight = towerScriptableObject.bulletOriginHeight;
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
