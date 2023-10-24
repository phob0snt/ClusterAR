using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangerManager : GameMode
{

    public static BangerManager Instance;
    [SerializeField] public List<BangerHead> heads;

    private void OnEnable()
    {
        BangerHead.onCollision += StartRaisingHeads;
    }

    private void OnDisable()
    {
        BangerHead.onCollision -= StartRaisingHeads;
    }

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
        StartRaisingHeads();
        BangerHead.isRaising = true;
        GameManager.HideMainMenu(true);
    }

    public override void EndSession()
    {
        BangerHead.isRaising = false;
        StartCoroutine(CustomSceneManager.ExitToMainMenu("Banger"));
        GameManager.HideMainMenu(false);
    }    

    private void StartRaisingHeads()
    {
        BangerHead tempHead = RandHead.GetRandHead(heads);
        tempHead.RaiseHead();
    }
}
