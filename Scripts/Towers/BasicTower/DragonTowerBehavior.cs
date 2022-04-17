using UnityEngine;

public class DragonTowerBehavior : TowerBehaviorBase
{
    new protected void Awake()
    {
        base.Awake();
        _transform.position += new Vector3(0, 5, 0);
        _transform.GetChild(0).localPosition = new Vector3(0, -5, 0);
    }
}