using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandWaveController : MonoBehaviour
{
    public static HandWaveController Instance;

    public static UnityEvent<Waver.Dirs> OnWavingHand = new();

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

    private GameObject hand;
    private void Update()
    {
        if (hand == null)
        {
            if (GameObject.Find("Left_ConicalGrabPointer(Clone)"))
            {
                hand = GameObject.Find("Left_ConicalGrabPointer(Clone)");
                hand.AddComponent<HandAxisCalculator>();
            }
        }
        else
        {
            //if (hand.transform.rotation.eulerAngles.z > 175 && hand.transform.rotation.eulerAngles.z < 210)
            //{
            //    test.axis = test.Axis.Y;
            //    if (test.handSpeed > 1)
            //        Debug.Log("up");
            //}
            //if (hand.transform.rotation.eulerAngles.z > 0 && hand.transform.rotation.eulerAngles.z < 30)
            //{
            //    test.axis = test.Axis.Y;
            //    if (test.handSpeed < -1)
            //        Debug.Log("down");
            //}
            //if (hand.transform.rotation.eulerAngles.y > 70 && hand.transform.rotation.eulerAngles.y < 200)
            //{
            //    test.axis = test.Axis.X;
            //    if (test.handSpeed > 1)
            //        Debug.Log("right");
            //}
            //if ((hand.transform.rotation.eulerAngles.y > 290 || hand.transform.rotation.eulerAngles.y < 10) && hand.transform.rotation.eulerAngles.z > 0 && hand.transform.rotation.eulerAngles.z < 150)
            //{
            //    test.axis = test.Axis.X;
            //    if (test.handSpeed < -1)
            //        Debug.Log("left");
            //}
            Vector3 surfaceNormal = hand.transform.up;
            Debug.DrawRay(hand.transform.position, surfaceNormal);


            float angle = Vector3.Angle(surfaceNormal, Vector3.up);

            if (angle < 45f)
            {
                HandAxisCalculator.axis = HandAxisCalculator.Axis.Y;
                if (HandAxisCalculator.handSpeed < -1)
                    OnWavingHand.Invoke(Waver.Dirs.Down);
            }
            else if (angle > 135f)
            {
                HandAxisCalculator.axis = HandAxisCalculator.Axis.Y;
                if (HandAxisCalculator.handSpeed > 1)
                    OnWavingHand.Invoke(Waver.Dirs.Up);
            }
            else if (surfaceNormal.x > 0f)
            {
                HandAxisCalculator.axis = HandAxisCalculator.Axis.X;
                if (HandAxisCalculator.handSpeed < -1)
                    OnWavingHand.Invoke(Waver.Dirs.Left);
            }
            else if (surfaceNormal.x < 0f)
            {
                HandAxisCalculator.axis = HandAxisCalculator.Axis.X;
                if (HandAxisCalculator.handSpeed > 1)
                    OnWavingHand.Invoke(Waver.Dirs.Right);
            }
            
        }
    }
}
