using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Layers
{
    private enum LayerName
    {
        Ground = 8,
        Unplaceable,
        MapCollider,
        Enemy
    }
    public static int Ground => 1 << (int)LayerName.Ground;
    public static int Unplaceable => 1 << (int)LayerName.Unplaceable;
    public static int MapCollider => 1 << (int)LayerName.MapCollider;
    public static int Enemy => 1 << (int)LayerName.Enemy;
}