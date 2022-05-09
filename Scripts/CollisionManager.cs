using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void Awake()
    {
        Physics.IgnoreLayerCollision(Layers.ProjectileRaw, Layers.ProjectileRaw);
        //Physics.IgnoreLayerCollision(Layers.ProjectileRaw, 0);
    }
}
