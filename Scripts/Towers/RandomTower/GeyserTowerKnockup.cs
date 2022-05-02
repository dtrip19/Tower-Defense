using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserTowerKnockup : MonoBehaviour
{
    private Enemy enemy;
    private Transform _transform;
    private float startingY;
    private float timeStarted;

    private void Awake()
    {
        if (TryGetComponent(out Enemy enemy))
        {
            this.enemy = enemy;
            enemy.canMove = false;
            _transform = transform;
            startingY = _transform.position.y;
            timeStarted = Time.time;
            if (!enemy.attributes.Contains(EnemyAttribute.Flying))
                enemy.attributes.Add(EnemyAttribute.Flying);
        }
        else
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        float time = Time.time;
        float timeSinceStarted = time - timeStarted;
        _transform.position = new Vector3(_transform.position.x, startingY + Mathf.Sin(timeSinceStarted * Mathf.PI) * 2, _transform.position.z);
        
        if (timeSinceStarted >= 1)
        {
            enemy.canMove = true;
            Destroy(this);
            enemy.attributes.Remove(EnemyAttribute.Flying);
        }
    }
}
