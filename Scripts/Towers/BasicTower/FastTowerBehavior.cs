using UnityEngine;

public class FastTowerBehavior : TowerBehaviorBase
{

    public void Start(){
        towerHeadTransform = model.transform.GetChild(0);
    }    
}
