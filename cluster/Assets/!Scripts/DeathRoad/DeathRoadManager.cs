using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRoadManager : GameMode
{
    public static DeathRoadManager Instance;
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
        GameManager.HideMainMenu(true);
    }

    public override void EndSession()
    {
        StartCoroutine(CustomSceneManager.ExitToMainMenu("DeathRoad"));
        GameManager.HideMainMenu(false);
    }
}
