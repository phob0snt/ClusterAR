using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waver"))
        {
            if (WaverManager.Instance.DoubleCoinsActive)
            {
                CoinCounter.CoinsAmount += 2;
                WaverSkins.Instance.IncreaseMoney(2);
            }
            else
            {
                CoinCounter.CoinsAmount++;
                WaverSkins.Instance.IncreaseMoney(1);
            }
            CoinCounter.Instance.UpdateCoinCounter(CoinCounter.CoinsAmount);
            WaverManager.Instance.SpawnCoin();
            Destroy(transform.parent.parent.gameObject);
        }  
    }
}
