using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    void FixedUpdate()
    {
        if (transform.position.y < -1)
            Destroy(gameObject);
    }
}
