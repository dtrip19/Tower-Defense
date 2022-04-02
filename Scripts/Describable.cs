using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Describable : MonoBehaviour
{
    public static event Action<TowerData> OnInspect;

    public void Inspect(TowerData data){
        OnInspect?.Invoke(data);
    }
}
