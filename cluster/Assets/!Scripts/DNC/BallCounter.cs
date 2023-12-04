using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCounter : MonoBehaviour
{
    public static int Score;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EggScript>() != null)
        {
            Score++;
            DNCSkins.Instance.IncreaseMoney(1);
            Destroy(other.gameObject);
        }
    }
}
