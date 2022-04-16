using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTowerBehavior : TowerBehaviorBase
{
    protected override DamageType DamageType => DamageType.Piercing;
}
