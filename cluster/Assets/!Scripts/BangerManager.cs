using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangerManager : GameMode
{
    public static BangerManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public override void ConfigureSession()
    {
        throw new System.NotImplementedException();
    }
}
