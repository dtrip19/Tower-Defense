using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UI
{
    public static void cursorEnter(TowerScriptableObject towerSO, Describable describable)
    {
        var towerData = new TowerData
        {
            towerName = towerSO.towerName,
            description = towerSO.description,
            price = towerSO.price,
            damage = towerSO.damage,
            attackDelay = towerSO.attackDelay,
            pierce = towerSO.pierce,
            bulletSpeed = towerSO.bulletSpeed,
            lifeTime = towerSO.lifeTime,
            range = towerSO.range,
            size = towerSO.colliderSize
        };

        describable.Inspect(towerData);
    }
}
