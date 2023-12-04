using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangerManager : GameMode
{

    public static BangerManager Instance;
    [SerializeField] public List<BangerHead> heads;
    [SerializeField] private GameObject hammer;
    [SerializeField] private Transform sceneCollider;
    [SerializeField] private GameObject sceneSetButton;

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
        hammer.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void StartSession()
    {
        StartRaisingHeads();
        BangerHead.isRaising = true;
        sceneSetButton.SetActive(false);
        hammer.GetComponent<Rigidbody>().isKinematic = false;
    }


    public override void ConfigureSession()
    {
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
