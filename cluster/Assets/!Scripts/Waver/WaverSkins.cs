using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaverSkins : MonoBehaviour
{
    public static WaverSkins Instance;
    private int _WaverMoney = 100;

    public enum Waver { Default, Smile, Heart, Russia }
    private Dictionary<Waver, int> waverPrices = new Dictionary<Waver, int>
    {
        { Waver.Default, 0},
        { Waver.Smile, 9},
        { Waver.Heart, 21},
        { Waver.Russia, 48},
    };


    private List<bool> boughtWavers = new() { true, false, false, false };

    [SerializeField] private List<GameObject> wavers;

    [SerializeField] private TMP_Text moneyText;

    private GameObject currentWaver;

    private void Awake()
    {
        currentWaver = wavers[0];
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool TryBuyItem(string itemName)
    {
        switch (itemName)
        {
            case "DefaultWaver":
                SetWaver(Waver.Default);
                return true;
            case "SmileWaver":
                if (!boughtWavers[(int)Waver.Smile])
                {
                    if (waverPrices[Waver.Smile] <= _WaverMoney)
                    {
                        DecreaseMoney(waverPrices[Waver.Smile]);
                        SetWaver(Waver.Smile);
                        boughtWavers[(int)Waver.Smile] = true;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetWaver(Waver.Smile);
                    return true;
                }
            case "HeartWaver":
                if (!boughtWavers[(int)Waver.Heart])
                {
                    if (waverPrices[Waver.Heart] <= _WaverMoney)
                    {
                        DecreaseMoney(waverPrices[Waver.Heart]);
                        SetWaver(Waver.Heart);
                        boughtWavers[(int)Waver.Heart] = true;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetWaver(Waver.Heart);
                    return true;
                }
            case "RussiaWaver":
                if (!boughtWavers[(int)Waver.Russia])
                {
                    if (waverPrices[Waver.Russia] <= _WaverMoney)
                    {
                        DecreaseMoney(waverPrices[Waver.Russia]);
                        SetWaver(Waver.Russia);
                        boughtWavers[(int)Waver.Russia] = true;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetWaver(Waver.Russia);
                    return true;
                }
            default:
                return false;
        }
    }

    private void UpdateMoney()
    {
        moneyText.text = $"Money: {_WaverMoney}";
    }

    public void IncreaseMoney(int amount)
    {
        _WaverMoney += amount;
        UpdateMoney();
    }

    private void DecreaseMoney(int amount)
    {
        _WaverMoney -= amount;
        UpdateMoney();
    }

    public void SetWaver(Waver waverType)
    {
        currentWaver = wavers[(int)waverType];
        WaverManager.Instance.RespawnWaver();
    }

    public GameObject GetWaver()
    {
        return currentWaver;
    }

}
