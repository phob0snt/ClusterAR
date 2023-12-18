using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandWaveController : MonoBehaviour
{
    public static HandWaveController Instance;

    public static UnityEvent<Waver.Dirs> OnWavingHand = new();

    private bool canWave;

    private GameObject hand;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    
    private void Update()
    {
        canWave = WaverManager.Instance.CanWave;
        if (hand == null)
        {
            if (GameObject.Find("Left_ConicalGrabPointer(Clone)"))
            {
                hand = GameObject.Find("Left_ConicalGrabPointer(Clone)");
                hand.AddComponent<HandAxisCalculator>();
            }
            else if (canWave)
            {
                if (Input.GetKeyDown(KeyCode.S) && !Waver.IsMoving)
                {
                    OnWavingHand.Invoke(Waver.Dirs.Down);
                }
                else if (Input.GetKeyDown(KeyCode.W) && !Waver.IsMoving)
                {
                    OnWavingHand.Invoke(Waver.Dirs.Up);
                }
                else if (Input.GetKeyDown(KeyCode.A) && !Waver.IsMoving)
                {
                    OnWavingHand.Invoke(Waver.Dirs.Left);
                }
                else if (Input.GetKeyDown(KeyCode.D) && !Waver.IsMoving)
                {
                    OnWavingHand.Invoke(Waver.Dirs.Right);
                }

            }

        }
        else if (canWave)
        {
            Vector3 surfaceNormal = hand.transform.up;
            Debug.DrawRay(hand.transform.position, surfaceNormal);


            float angle = Vector3.Angle(surfaceNormal, Vector3.up);

            if (angle < 45f && !Waver.IsMoving)
            {
                HandAxisCalculator.axis = HandAxisCalculator.Axis.Y;
                if (HandAxisCalculator.handSpeed < -1 && Mathf.Abs(HandAxisCalculator.handRotation) < 15)
                    OnWavingHand.Invoke(Waver.Dirs.Down);
            }
            else if (angle > 135f && !Waver.IsMoving)
            {
                HandAxisCalculator.axis = HandAxisCalculator.Axis.Y;
                if (HandAxisCalculator.handSpeed > 1 && Mathf.Abs(HandAxisCalculator.handRotation) < 15)
                    OnWavingHand.Invoke(Waver.Dirs.Up);
            }
            else if (surfaceNormal.x > 0f && angle > 45f && angle < 135 && !Waver.IsMoving)
            {
                HandAxisCalculator.axis = HandAxisCalculator.Axis.X;
                if (HandAxisCalculator.handSpeed < -1 && Mathf.Abs(HandAxisCalculator.handRotation) < 15)
                    OnWavingHand.Invoke(Waver.Dirs.Left);
            }
            else if (surfaceNormal.x < 0f && angle > 45f && angle < 135 && !Waver.IsMoving)
            {
                HandAxisCalculator.axis = HandAxisCalculator.Axis.X;
                if (HandAxisCalculator.handSpeed > 1 && Mathf.Abs(HandAxisCalculator.handRotation) < 15)
                    OnWavingHand.Invoke(Waver.Dirs.Right);
            }

        }
        
    }
}
