using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCounter : MonoBehaviour
{
    public static int Score;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MeshCollider>() != null)
        {
            Score++;
            Destroy(other.gameObject);
        }
    }
}
