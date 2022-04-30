using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserTowerKnockup : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        if (TryGetComponent(out Enemy enemy))
        {
            this.enemy = enemy;
        }
        else
        {
            Destroy(this);
        }
    }
}
