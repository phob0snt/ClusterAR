using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class RandHead
{
    public static BangerHead GetRandHead(List<BangerHead> heads)
    {
        System.Random rand = new System.Random();
        return heads[rand.Next(0, heads.Count)];
    }
}
