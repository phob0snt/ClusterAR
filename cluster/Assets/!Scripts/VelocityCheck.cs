using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class VelocityCheck : MonoBehaviour
{
    public float PrevFrameVel;
    private Vector3 prevPos;
    void FixedUpdate()
    {
        PrevFrameVel = (transform.position - prevPos).sqrMagnitude;
        prevPos = transform.position;
        Debug.Log(PrevFrameVel);
    }
}
