using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangerShop : Shop
{
    [SerializeField] private GameObject typesButts;
    [SerializeField] private GameObject hammerButts;
    [SerializeField] private GameObject headButts;
    public override void ToggleItems(string item)
    {
        switch (item)
        {
            case "hammer":
                hammerButts.SetActive(true);
                headButts.SetActive(false);
                break;
            case "heads":
                hammerButts.SetActive(false);
                headButts.SetActive(true);
                break;
        }
        typesButts.SetActive(false);
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
            hammerButts.SetActive(false);
            headButts.SetActive(false);
        }
    }
}
