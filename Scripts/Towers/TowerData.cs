using System.Collections.Generic;
using UnityEngine;

public struct TowerData
{
    public List<TowerScriptableObject> upgrades;
    public GameObject bullet;

    public string towerName;
    public string description; 
    public int price;
    public int damage;
    public int attackDelay;
    public int pierce;
    public int ammo;
    public int towerID;
    public float bulletOriginHeight;
    public float bulletSpeed;
    public float lifeTime;
    public float range;
    public float size;
}
