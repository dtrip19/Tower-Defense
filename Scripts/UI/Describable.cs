using UnityEngine;
using System;

public class Describable : MonoBehaviour
{
    public static event Action<TowerData> OnInspect;
    public static event Action OnUninspect;

    public void Inspect(TowerData data)
    {
        OnInspect?.Invoke(data);
    }

    public void Uninspect()
    {
        OnUninspect?.Invoke();
    }
}
