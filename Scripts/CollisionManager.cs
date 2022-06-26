using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void Awake()
    {
        Physics.IgnoreLayerCollision(Layers.ProjectileRaw, Layers.ProjectileRaw);
        Physics.IgnoreLayerCollision(Layers.UnplaceableRaw, Layers.UnplaceableRaw);
        Physics.IgnoreLayerCollision(Layers.UnplaceableRaw, Layers.GroundRaw);
        Physics.IgnoreLayerCollision(Layers.GroundRaw, Layers.GroundRaw);
        Physics.IgnoreLayerCollision(Layers.UnplaceableRaw, 0);
        //Physics.IgnoreLayerCollision(Layers.ProjectileRaw, 0);
    }
}
