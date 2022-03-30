using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers")]
public class TowerScriptableObject : ScriptableObject
{
    public float bulletSpeed;
    public GameObject bullet;
    public float bulletLifeTime;
    public int bulletDamage;
    public int attackSpeed;
    // Start is called before the first frame update

}
