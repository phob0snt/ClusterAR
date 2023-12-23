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

    public void RespawnHammer()
    {
        GameObject tempHammer = hammer;
        hammer = Instantiate(BangerSkins.Instance.GetHammer(), tempHammer.transform.position, Quaternion.identity);
        hammer.transform.SetParent(tempHammer.transform.parent, true);
        Destroy(tempHammer);
    }

    public void ChangeHeads()
    {
        int headsAmount = heads.Count;
        for (int i = 0; i < headsAmount; i++)
        {
            GameObject tempHead = heads[i].gameObject;
            GameObject newHead = Instantiate(BangerSkins.Instance.GetHead(), tempHead.transform.position, Quaternion.identity);
            newHead.transform.SetParent(tempHead.transform.parent, true);
            newHead.GetComponent<BangerHead>().currHeadPos = tempHead.GetComponent<BangerHead>().currHeadPos;
            heads[i] = newHead.GetComponent<BangerHead>();
            Destroy(tempHead);
        }
    }

    private void StartRaisingHeads()
    {
        BangerHead tempHead = RandHead.GetRandHead(heads);
        tempHead.RaiseHead();
    }
}
