
public static class UI
{
    public static void CursorEnter(TowerScriptableObject towerSO, Describable describable)
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

    public static TowerData GetTowerDataFromSO(TowerScriptableObject towerSO)
    {
        var towerData = new TowerData
        {
            towerName = towerSO.towerName,
            description = towerSO.description,
            price = towerSO.price,
            damage = towerSO.damage,
            attackDelay = towerSO.attackDelay,
            pierce = towerSO.pierce,
            bullet = towerSO.bullet,
            bulletSpeed = towerSO.bulletSpeed,
            bulletOriginHeight = towerSO.bulletOriginHeight,
            lifeTime = towerSO.lifeTime,
            range = towerSO.range,
            size = towerSO.colliderSize,
            ammo = towerSO.ammo
        };

        return towerData;
    }
}
