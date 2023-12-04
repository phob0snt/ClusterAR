using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRoadManager : GameMode
{
    public static DeathRoadManager Instance;

    [SerializeField] private GameObject sceneSetButton;

    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private FrogScript frogScript;
    [SerializeField] private Moving moving;
    [SerializeField] private WoodProtector wp;
    [SerializeField] private StoneProtector sp;
    [SerializeField] private CloudProtector cp;
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
        mapGenerator.Generate();
        frogScript.sceneIsSet = true;
        moving.CanMove = true;
        wp.SetPos();
        sp.SetPos();
        cp.SetPos();
    }

    public override void EndSession()
    {
        StartCoroutine(CustomSceneManager.ExitToMainMenu("DeathRoad"));
        GameManager.HideMainMenu(false);
    }
}
