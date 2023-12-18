using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DNCSkins : MonoBehaviour
{
    public static DNCSkins Instance;
    private int _DNCMoney;

    public enum Bucket { Default, Rusty, Normal, Bright}
    public enum Egg { Default, Brown, Green, Rainbow }
    private Dictionary<Bucket, int> bucketsPrices = new Dictionary<Bucket, int>
    {
        { Bucket.Default, 0},
        { Bucket.Rusty, 10},
        { Bucket.Normal, 26},
        { Bucket.Bright, 58}

    };

    private Dictionary<Egg, int> eggsPrices = new Dictionary<Egg, int>
    {
        { Egg.Default, 0},
        { Egg.Brown, 8},
        { Egg.Green, 20},
        { Egg.Rainbow, 50}
    };

    private List<bool> boughtEggs = new () { true, false, false, false };
    private List<bool> boughtBuckets = new() { true, false, false, false };

    [SerializeField] private List<GameObject> buckets;
    [SerializeField] private List<GameObject> eggs;

    [SerializeField] private TMP_Text moneyText;

    private GameObject currentBucket;
    private GameObject currentEgg;

    private void Awake()
    {
        currentEgg = eggs[0];
        currentBucket = buckets[0];
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
            case "DefaultEgg":
                SetEgg(Egg.Default);
                return true;
            case "BrownEgg":
                if (!boughtEggs[(int)Egg.Brown])
                {
                    if (eggsPrices[Egg.Brown] <= _DNCMoney)
                    {
                        DecreaseMoney(eggsPrices[Egg.Brown]);
                        SetEgg(Egg.Brown);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetEgg(Egg.Brown);
                    return true;
                }
            case "GreenEgg":
                if (!boughtEggs[(int)Egg.Green])
                {
                    if (eggsPrices[Egg.Green] <= _DNCMoney)
                    {
                        DecreaseMoney(eggsPrices[Egg.Green]);
                        SetEgg(Egg.Green);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetEgg(Egg.Green);
                    return true;
                }
            case "RainbowEgg":
                if (!boughtEggs[(int)Egg.Rainbow])
                {
                    if (eggsPrices[Egg.Rainbow] <= _DNCMoney)
                    {
                        DecreaseMoney(eggsPrices[Egg.Rainbow]);
                        SetEgg(Egg.Rainbow);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetEgg(Egg.Rainbow);
                    return true;
                }
            case "DefaultBucket":
                SetBucket(Bucket.Default);
                return true;
            case "RustyBucket":
                if (!boughtBuckets[(int)Bucket.Rusty])
                {
                    if (bucketsPrices[Bucket.Rusty] <= _DNCMoney)
                    {
                        DecreaseMoney(bucketsPrices[Bucket.Rusty]);
                        SetBucket(Bucket.Rusty);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetBucket(Bucket.Rusty);
                    return true;
                }
            case "NormalBucket":
                if (!boughtBuckets[(int)Bucket.Normal])
                {
                    if (bucketsPrices[Bucket.Normal] <= _DNCMoney)
                    {
                        DecreaseMoney(bucketsPrices[Bucket.Normal]);
                        SetBucket(Bucket.Normal);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetBucket(Bucket.Normal);
                    return true;
                }
            case "BrightBucket":
                if (!boughtBuckets[(int)Bucket.Bright])
                {
                    if (bucketsPrices[Bucket.Bright] <= _DNCMoney)
                    {
                        DecreaseMoney(bucketsPrices[Bucket.Bright]);
                        SetBucket(Bucket.Bright);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetBucket(Bucket.Bright);
                    return true;
                }
            default:
                return false;
        }
    }

    private void UpdateMoney()
    {
        moneyText.text = $"Money: {_DNCMoney}";
    }

    public void IncreaseMoney(int amount)
    {
        _DNCMoney += amount;
        UpdateMoney();
    }

    private void DecreaseMoney(int amount)
    {
        _DNCMoney -= amount;
        UpdateMoney();
    }

    public void SetEgg(Egg eggType)
    {
        currentEgg = eggs[(int)eggType];
    }

    public void SetBucket(Bucket bucketType)
    {
        currentBucket = buckets[(int)bucketType];
        DNCManager.Instance.RespawnBucket();
    }

    public GameObject GetBucket()
    {
        return currentBucket;
    }

    public GameObject GetEgg()
    {
        return currentEgg;
    }
}
