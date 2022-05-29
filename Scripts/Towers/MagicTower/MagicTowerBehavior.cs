using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTowerBehavior : TowerBehaviorBase
{

    protected override DamageType DamageType => DamageType.Elemental;
}
