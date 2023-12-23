using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCrashArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BonusSphere>() != null)
        {
            other.GetComponent<BonusSphere>().ApplyBonus();
            Destroy(other.gameObject);
        } 
    }
}
