using System.Collections.Generic;
using UnityEngine;

public struct TowerData
{
    public List<TowerScriptableObject> upgrades;
    public GameObject bullet;

    public string description; 
    public int price;
    public int damage;
    public int attackDelay;
    public int pierce;
    public float bulletSpeed;
    public float lifeTime;
    public float range;
    public float size;
}
