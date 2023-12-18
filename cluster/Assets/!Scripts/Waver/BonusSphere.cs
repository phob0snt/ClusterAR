using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusSphere : MonoBehaviour
{
    public enum BonusType { Double, }
    public abstract void ApplyBonus();
}
