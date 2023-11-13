using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenu : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    public void ExitGamemode()
    {
        switch (GameManager.CurrentGamemode)
        {
            case (DNCManager):
                DNCManager.Instance.EndSession();
                break;
            case (BangerManager):
                BangerManager.Instance.EndSession();
                break;
            case (DeathRoadManager):
                DeathRoadManager.Instance.EndSession();
                break;
            case (WaverManager):
                WaverManager.Instance.EndSession();
                break;
        }
        
    }

    public void OpenShop()
    {
        shop.SetActive(true);
    }
}
