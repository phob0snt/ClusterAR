using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNCShop : Shop
{
    [SerializeField] private GameObject typesButts;
    [SerializeField] private GameObject eggButts;
    [SerializeField] private GameObject bucketButts;
    public override void ToggleItems(string item)
    {
        switch (item)
        {
            case "eggs":
                eggButts.SetActive(true);
                typesButts.SetActive(false);
                break;
            case "buckets":
                bucketButts.SetActive(true);
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
            bucketButts.SetActive(false);
            eggButts.SetActive(false);
        }
        
    }
}
