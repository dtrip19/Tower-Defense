using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Describable : MonoBehaviour
{
    public static event Action<string> OnInspect;

    public void Inspect(string description){
        OnInspect?.Invoke(description);
    }
}
