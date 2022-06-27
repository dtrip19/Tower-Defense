using UnityEngine;

public class OncePunchTowerBehavior : TowerBehaviorBase
{
    protected override DamageType DamageType => DamageType.Piercing;


    public void Start(){
        towerHeadTransform = _transform;
    }

    new protected void FixedUpdate()
    {
        timer++;
        if (ammo > 0 && timer >= attackDelay && target != null && canShoot)
        {
            timer = 0;
            ammo = Mathf.Clamp(ammo - target.Health, 0, 10000);
            Shoot();
        }
    }

    protected override void Shoot()
    {
        target.TakeDamage(damage, DamageType);
    }
}