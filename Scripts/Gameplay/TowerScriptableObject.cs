using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers")]
public class TowerScriptableObject : ScriptableObject
{
    public List<TowerScriptableObject> upgrades;
    public GameObject bullet;
    public Sprite icon;

    public string description;
    public int damage;
    public int attackDelay;
    public int price;
    public float bulletSpeed;
    public float lifeTime;
    public float range;
    public float colliderSize;
    public float bulletOriginHeight;
}
