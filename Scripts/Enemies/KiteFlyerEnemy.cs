using System;
using UnityEngine;

public class KiteFlyerEnemy : MonoBehaviour
{
    private Enemy self;
    private Enemy kiteEnemy;

    public static event Action<Enemy> OnKiteDestroyed;

    private void Awake()
    {
        self = GetComponent<Enemy>();
        kiteEnemy = transform.GetChild(0).GetChild(0).GetComponent<Enemy>();
        kiteEnemy.OnDestroyed += Enrage;
    }

    private void Enrage()
    {
        OnKiteDestroyed?.Invoke(self);
    }
}
