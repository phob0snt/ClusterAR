using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Protector : MonoBehaviour
{
    public static Action SuccessGoing;
    public static Action UnSuccessGoing;
    public enum ProtectorType
    {
        Stone,
        Wood,
        Cloud
    }
    public Dictionary<ProtectorType, string> ProtectorPairs = new Dictionary<ProtectorType, string>
    {
        { ProtectorType.Stone, "DangerLava" },
        { ProtectorType.Wood, "DangerWater" },
        { ProtectorType.Cloud, "DangerHole" }
    };

    public abstract ProtectorType type { get; }
    public abstract IEnumerator ApplyProtector(bool correct);

}
