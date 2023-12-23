using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseShopItem : MonoBehaviour
{
    [SerializeField] GameObject notEnoughText;
    public void TryToBuy(string item)
    {
        switch (GameManager.CurrentGamemode)
        {
            case DNCManager:
                if (!DNCSkins.Instance.TryBuyItem(item))
                    StartCoroutine(ShowNEMText());
                break;
            case BangerManager:
                if (!BangerSkins.Instance.TryBuyItem(item))
                    StartCoroutine(ShowNEMText());
                break;
            case DeathRoadManager:
                if (!DeathRoadSkins.Instance.TryBuyItem(item))
                    StartCoroutine(ShowNEMText());
                break;
            case WaverManager:
                if (!WaverSkins.Instance.TryBuyItem(item))
                    StartCoroutine(ShowNEMText());
                break;
        }
            
    }

    private IEnumerator ShowNEMText()
    {
        notEnoughText.SetActive(true);
        yield return new WaitForSeconds(2);
        notEnoughText.SetActive(false);
    }
}
