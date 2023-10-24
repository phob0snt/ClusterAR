using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRoadShop : Shop
{
    [SerializeField] private GameObject typesButts;
    public override void ToggleItems(string item)
    {
        switch (item)
        {
            case "":
                typesButts.SetActive(false);
                break;
            case " ":
                typesButts.SetActive(false);
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
        }

    }
}
