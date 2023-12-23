using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaverShop : Shop
{
    [SerializeField] private GameObject typesButts;
    [SerializeField] private GameObject waverButts;
    public override void ToggleItems(string item)
    {
        switch (item)
        {
            case "Waver":
                waverButts.SetActive(true);
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
            waverButts.SetActive(false);
        }
    }
}
