using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAxisCalculator : MonoBehaviour
{
    public static HandAxisCalculator Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public enum Axis { X, Y, Z };
    public static Axis axis;

    private float previousPosition;
    public static float handSpeed;

    void Start()
    {
        switch (axis)
        {
            case Axis.X:
                previousPosition = transform.position.x;
                break;
            case Axis.Y:
                previousPosition = transform.position.y;
                break;
            case Axis.Z:
                previousPosition = transform.position.z;
                break;
        }
    }

    void FixedUpdate()
    {
        float currentPosition;
        switch (axis)
        {
            case Axis.X:
                currentPosition = transform.position.x;
                break;
            case Axis.Y:
                currentPosition = transform.position.y;
                break;
            case Axis.Z:
                currentPosition = transform.position.z;
                break;
            default:
                currentPosition = 0f;
                break;
        }

        float displacement = currentPosition - previousPosition;
        handSpeed = displacement / Time.deltaTime;

        previousPosition = currentPosition;

    }
}
