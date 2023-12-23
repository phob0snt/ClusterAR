using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathRoadSkins : MonoBehaviour
{
    public static DeathRoadSkins Instance;
    private int _DeathRoadMoney = 100;

    public enum Player { Default, Cat }

    private Dictionary<Player, int> playerPrices = new Dictionary<Player, int>
    {
        { Player.Default, 0},
        { Player.Cat, 78}
    };

    private List<bool> boughtPlayers = new() { true, false };

    [SerializeField] private List<GameObject> players;

    [SerializeField] private TMP_Text moneyText;

    private GameObject currentPlayer;

    private void Awake()
    {
        currentPlayer = players[0];
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
            case "DefaultPlayer":
                SetPlayer(Player.Default);
                return true;
            case "CatPlayer":
                if (!boughtPlayers[(int)Player.Cat])
                {
                    if (playerPrices[Player.Cat] <= _DeathRoadMoney)
                    {
                        DecreaseMoney(playerPrices[Player.Cat]);
                        SetPlayer(Player.Cat);
                        boughtPlayers[(int)Player.Cat] = true;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    SetPlayer(Player.Cat);
                    return true;
                }
            default:
                return false;
        }
    }

    private void UpdateMoney()
    {
        moneyText.text = $"Money: {_DeathRoadMoney}";
    }

    public void IncreaseMoney(int amount)
    {
        _DeathRoadMoney += amount;
        UpdateMoney();
    }

    private void DecreaseMoney(int amount)
    {
        _DeathRoadMoney -= amount;
        UpdateMoney();
    }

    public void SetPlayer(Player playerType)
    {
        currentPlayer = players[(int)playerType];
        StartCoroutine(DeathRoadManager.Instance.RespawnPlayer());
    }

    public GameObject GetPlayer()
    {
        return currentPlayer;
    }
}
