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
    private float previousRotation;
    public static float handSpeed;
    public static float handRotation;

    void Start()
    {
        switch (axis)
        {
            case Axis.X:
                previousRotation = transform.eulerAngles.x;
                previousPosition = transform.position.x;
                break;
            case Axis.Y:
                previousRotation = transform.eulerAngles.y;
                previousPosition = transform.position.y;
                break;
            case Axis.Z:
                previousRotation = transform.eulerAngles.z;
                previousPosition = transform.position.z;
                break;
        }
    }

    void FixedUpdate()
    {
        float currentPosition;
        float currentRotation;
        switch (axis)
        {
            case Axis.X:
                currentRotation = transform.eulerAngles.x;
                currentPosition = transform.position.x;
                break;
            case Axis.Y:
                currentRotation = transform.eulerAngles.y;
                currentPosition = transform.position.y;
                break;
            case Axis.Z:
                currentRotation = transform.eulerAngles.z;
                currentPosition = transform.position.z;
                break;
            default:
                currentRotation = 0f;
                currentPosition = 0f;
                break;
        }
        Debug.DrawRay(transform.position, transform.forward, Color.magenta);
        float displacement = currentPosition - previousPosition;
        float rotated = currentRotation - previousRotation;
        handSpeed = displacement / Time.deltaTime;
        handRotation = rotated / Time.deltaTime;
        Debug.Log($"Spped: {handSpeed}, Rotation: {handRotation}");

        previousPosition = currentPosition;
        previousRotation = currentRotation;
    }
}
