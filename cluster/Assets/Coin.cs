using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waver"))
        {
            CoinCounter.CoinsAmount++;
            CoinCounter.Instance.UpdateCoinCounter(CoinCounter.CoinsAmount);
        }  
        Destroy(gameObject);
    }
}
