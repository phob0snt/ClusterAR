using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance;
    [SerializeField] private TMP_Text coinsText;
    public static int CoinsAmount = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    

    public void UpdateCoinCounter(int coinsAmount)
    {
        coinsText.text = coinsAmount.ToString();
        Debug.Log("PENIS");
    }
}
