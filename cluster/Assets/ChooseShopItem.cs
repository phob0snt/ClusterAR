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
                break;
            case DeathRoadManager:
                break;
            case WaverManager:
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
