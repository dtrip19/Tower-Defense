
public class BasicTowerBehavior : TowerBehaviorBase
{

    override public void FaceTarget(){
        model.transform.GetChild(2).GetChild(0).GetChild(1).LookAt(target.transform);
    }

}
