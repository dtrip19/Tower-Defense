using UnityEngine;

public class TrackingProjectile : Projectile
{
    private Transform target;

    new private void Awake()
    {
        base.Awake();
        collidesWithSurfaces = false;
    }

    public override void SetValues(Vector3 direction, DamageType damageType, float timeDestroy, float speed, int damage, int pierce)
    {
        base.SetValues(direction, damageType, timeDestroy, speed, damage, pierce);

        if (Physics.Raycast(_transform.position, direction, out RaycastHit hit, 100, Layers.Enemy))
        {
            target = hit.transform;
        }
    }

    new private void FixedUpdate()
    {
        base.FixedUpdate();

        if (target == null || target.Equals(null)) return;

        var dirToTarget = target.position + new Vector3(0, 1, 0) - _transform.position;
        var dot = Vector3.Dot(direction.normalized, dirToTarget.normalized);
        //print(dot.ToString());
        if (dot >= 0.85f)
            direction = Vector3.Slerp(direction, dirToTarget, 0.2f);
    }
}
