using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoinsSphere : BonusSphere
{
    public override void ApplyBonus()
    {
        WaverManager.Instance.DoubleCoins();
    }
}
