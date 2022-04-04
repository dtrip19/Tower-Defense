using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers")]
public class TowerScriptableObject : ScriptableObject
{
    public GameObject bullet;
    public float bulletSpeed;
    public float lifeTime;
    public int damage;
    public int attackDelay;
    public int price;

    public float range;
    public float colliderSize;
    public string description; 
    // Start is called before the first frame update

}
