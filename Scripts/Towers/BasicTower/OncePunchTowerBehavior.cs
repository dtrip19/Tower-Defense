
public class OncePunchTowerBehavior : TowerBehaviorBase
{
    protected override DamageType DamageType => DamageType.Explosive;

    protected override void Shoot()
    {
        target.TakeDamage(damage, DamageType);
    }
}