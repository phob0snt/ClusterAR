using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BangerSkins : MonoBehaviour
{
    public static BangerSkins Instance;
    private int _BangerMoney = 100;

    public enum Hammer { Default, Steel, Gold }
    public enum Head { Default, Clown, Skull }

    private Dictionary<Hammer, int> hammerPrices = new Dictionary<Hammer, int>
    {
        { Hammer.Default, 0},
        { Hammer.Steel, 12},
        { Hammer.Gold, 34},
    };

    private Dictionary<Head, int> headPrices = new Dictionary<Head, int>
    {
        { Head.Default, 0},
        { Head.Clown, 18},
        { Head.Skull, 40},
    };


    private List<bool> boughtHammers = new() { true, false, false };
    private List<bool> boughtHeads = new() { true, false, false };

    [SerializeField] private List<GameObject> hammers;
    [SerializeField] private List<GameObject> heads;

    [SerializeField] private TMP_Text moneyText;

    private GameObject currentHammer;
    private GameObject currentHead;

    private void Awake()
    {
        currentHammer = hammers[0];
        currentHead = heads[0];
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
            case "DefaultHammer":
                SetHammer(Hammer.Default);
                return true;
            case "SteelHammer":
                if (!boughtHammers[(int)Hammer.Steel])
                {
                    if (hammerPrices[Hammer.Steel] <= _BangerMoney)
                    {
                        DecreaseMoney(hammerPrices[Hammer.Steel]);
                        SetHammer(Hammer.Steel);
                        boughtHammers[(int)Hammer.Steel] = true;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetHammer(Hammer.Steel);
                    return true;
                }
            case "GoldHammer":
                if (!boughtHammers[(int)Hammer.Gold])
                {
                    if (hammerPrices[Hammer.Gold] <= _BangerMoney)
                    {
                        DecreaseMoney(hammerPrices[Hammer.Gold]);
                        SetHammer(Hammer.Gold);
                        boughtHammers[(int)Hammer.Gold] = true;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetHammer(Hammer.Gold);
                    return true;
                }
            case "DefaultHead":
                SetHead(Head.Default);
                return true;
            case "ClownHead":
                if (!boughtHeads[(int)Head.Clown])
                {
                    if (headPrices[Head.Clown] <= _BangerMoney)
                    {
                        DecreaseMoney(headPrices[Head.Clown]);
                        SetHead(Head.Clown);
                        boughtHeads[(int)Head.Clown] = true;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetHead(Head.Clown);
                    return true;
                }
            case "SkullHead":
                if (!boughtHeads[(int)Head.Skull])
                {
                    if (headPrices[Head.Skull] <= _BangerMoney)
                    {
                        DecreaseMoney(headPrices[Head.Skull]);
                        SetHead(Head.Skull);
                        boughtHeads[(int)Head.Skull] = true;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetHead(Head.Skull);
                    return true;
                }
            default:
                return false;
        }
    }

    private void UpdateMoney()
    {
        moneyText.text = $"Money: {_BangerMoney}";
    }

    public void IncreaseMoney(int amount)
    {
        _BangerMoney += amount;
        UpdateMoney();
    }

    private void DecreaseMoney(int amount)
    {
        _BangerMoney -= amount;
        UpdateMoney();
    }

    public void SetHammer(Hammer hammerType)
    {
        currentHammer = hammers[(int)hammerType];
        BangerManager.Instance.RespawnHammer();
    }

    public void SetHead(Head headType)
    {
        currentHead = heads[(int)headType];
        BangerManager.Instance.ChangeHeads();
    }

    public GameObject GetHead()
    {
        return currentHead;
    }

    public GameObject GetHammer()
    {
        return currentHammer;
    }


}
