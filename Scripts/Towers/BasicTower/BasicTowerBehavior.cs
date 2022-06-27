
public class BasicTowerBehavior : TowerBehaviorBase
{
    public void Start(){
        towerHeadTransform = model.transform.GetChild(2).GetChild(0).GetChild(1);
    }   
}
