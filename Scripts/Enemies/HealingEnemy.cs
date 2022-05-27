using UnityEngine;

public class HealingEnemy : MonoBehaviour
{
    private Transform _transform;
    private int ticks;

    private void Awake()
    {
        _transform = transform;
        Instantiate(Resources.Load<GameObject>("Enemies/HealingField"), _transform);
    }

    private void FixedUpdate()
    {
        if (ticks++ > 50)
        {
            var colliders = Physics.OverlapSphere(_transform.position, 6, Layers.Enemy);
            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent(out Enemy enemy)) continue;

                enemy.Heal(5);
            }

            ticks = 0;
        }
    }
}
