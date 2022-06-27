
public class FastTowerBehavior : TowerBehaviorBase
{
    override public void FaceTarget(){
        model.transform.GetChild(0).LookAt(target.transform);
    }
}
