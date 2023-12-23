using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRoadShop : Shop
{
    [SerializeField] private GameObject typesButts;
    [SerializeField] private GameObject playerButts;
    public override void ToggleItems(string item)
    {
        switch (item)
        {
            case "player":
                typesButts.SetActive(false);
                playerButts.SetActive(true);
                break;
        }

    }

    public override void GoBack()
    {
        if (typesButts.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            typesButts.SetActive(true);
            playerButts.SetActive(false);
        }

    }
}
