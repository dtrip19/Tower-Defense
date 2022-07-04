using UnityEngine;

public class TornadoTowerBehavior : TowerBehaviorBase
{
    protected override void Shoot()
    {
        int enemiesHit = 0;

        var colliders = Physics.OverlapSphere(_transform.position, range, Layers.Enemy);
        foreach (var collider in colliders)
        {
            if (enemiesHit++ > pierce) return;

            if (collider.TryGetComponent(out Enemy enemy))
            {
                int damage = this.damage;
                if (enemy.attributes.Contains(EnemyAttribute.Flying))
                    damage *= 2;
                enemy.TakeDamage(damage, DamageType.Normal);
            }
        }
    }

    new protected void Update(){
        base.Update();
        _transform.Rotate(0,1.5f,0);
    }
}
