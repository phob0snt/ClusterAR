using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoinsBooster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waver"))
        {
            WaverManager.Instance.SpawnSphere(BonusSphere.BonusType.Double);
            Destroy(gameObject);
        }
    }
}
