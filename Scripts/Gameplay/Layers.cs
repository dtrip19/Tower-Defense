
public static class Layers
{
    private enum LayerName
    {
        Ground = 8,
        Unplaceable,
        MapCollider,
        Enemy,
        Projectile,
        Tower
    }

    public static int Ground => 1 << (int)LayerName.Ground;
    public static int Unplaceable => 1 << (int)LayerName.Unplaceable;
    public static int MapCollider => 1 << (int)LayerName.MapCollider;
    public static int Enemy => 1 << (int)LayerName.Enemy;
    public static int Projectile => 1 << (int)LayerName.Projectile;
    public static int Tower => 1 << (int)LayerName.Tower;

    public static int GroundRaw => (int)LayerName.Ground;
    public static int UnplaceableRaw => (int)LayerName.Unplaceable;
    public static int MapColliderRaw => (int)LayerName.MapCollider;
    public static int EnemyRaw => (int)LayerName.Enemy;
    public static int ProjectileRaw => (int)LayerName.Projectile;
    public static int TowerRaw => (int)LayerName.Tower;
}