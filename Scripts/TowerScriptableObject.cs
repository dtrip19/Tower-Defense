using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers")]
public class TowerScriptableObject : ScriptableObject
{
    public float speed;
    public GameObject bullet;
    public float lifeTime;
    public int damage;
    public int attackDelay;
    public int price;
    public string description; 
    // Start is called before the first frame update

}
