using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangerShop : Shop
{
    [SerializeField] private GameObject typesButts;
    [SerializeField] private GameObject headButts;
    [SerializeField] private GameObject deskButts;
    public override void ToggleItems(string item)
    {
        switch (item)
        {
            case "heads":
                headButts.SetActive(true);
                typesButts.SetActive(false);
                break;
            case "desk":
                deskButts.SetActive(true);
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
            deskButts.SetActive(false);
            headButts.SetActive(false);
        }
    }
}
