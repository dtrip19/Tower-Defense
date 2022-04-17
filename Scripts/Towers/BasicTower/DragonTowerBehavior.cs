using UnityEngine;

public class DragonTowerBehavior : TowerBehaviorBase
{
    new protected void Awake()
    {
        base.Awake();
        _transform.position += new Vector3(0, 5, 0);
    }
}