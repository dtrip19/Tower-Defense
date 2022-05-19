using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower")]
public class TowerScriptableObject : ScriptableObject
{
    public List<TowerScriptableObject> upgrades;
    public GameObject model;
    public GameObject bullet;
    public Sprite icon;
    public string towerName;
    public string description;
    public int damage;
    public int attackDelay;
    public int price;
    public int pierce;
    public int ammo;
    public float bulletSpeed;
    public float lifeTime;
    public float range;
    public float colliderSize;
    public float bulletOriginHeight;
    public uint towerId;
}
