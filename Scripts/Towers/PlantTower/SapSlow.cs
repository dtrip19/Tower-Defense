using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapSlow : MonoBehaviour
{
    public int ticks = 200;
    private Enemy enemy;

    private void Awake()
    {
        if (TryGetComponent(out Enemy enemy))
        {
            this.enemy = enemy;
            enemy.moveSpeedMultiplier = 0.6f;
        }
        else
            Destroy(this);
    }

    private void FixedUpdate()
    {
        if (ticks-- <= 0)
            Destroy(this);
    }

    private void OnDestroy()
    {
        enemy.moveSpeedMultiplier = 1;
    }
}
