using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaverManager : GameMode
{
    public static WaverManager Instance;
    [SerializeField] private GameObject sceneSetButton;
    public bool CanWave;

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

    public void StartSession()
    {
        sceneSetButton.SetActive(false);
        CanWave = true;
    }

    public override void EndSession()
    {
        StartCoroutine(CustomSceneManager.ExitToMainMenu("Waver"));
        GameManager.HideMainMenu(false);
    }
}
