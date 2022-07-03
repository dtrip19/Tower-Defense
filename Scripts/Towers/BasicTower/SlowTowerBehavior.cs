
public class SlowTowerBehavior : TowerBehaviorBase
{
    protected override DamageType DamageType => DamageType.Piercing;

    public void Start(){
        towerHeadTransform = model.transform.GetChild(1);
    }   
}
