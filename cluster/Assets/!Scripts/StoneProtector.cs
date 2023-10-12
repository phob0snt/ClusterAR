using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoneProtector : Protector
{
    public override ProtectorType type => ProtectorType.Stone;
    private RaycastHit hit;
    private bool isMoving;

    void Update()
    {
        Debug.DrawRay(transform.localPosition, -transform.up * 0.083f);
        if (!isMoving)
        {
            Physics.Raycast(transform.position, -transform.up, out hit, 0.083f);
            try
            {
                if (hit.transform.gameObject.CompareTag(ProtectorPairs.GetValueOrDefault(ProtectorType.Stone)))
                {
                    StartCoroutine(ApplyProtector(true));
                }
                else if (ProtectorPairs.Values.Contains(hit.transform.gameObject.tag))
                {
                    StartCoroutine(ApplyProtector(false));
                }
            }
            catch { }
        } 
    }


    public override IEnumerator ApplyProtector(bool correct)
    {
        isMoving = true;
        float totalMovementTime = 70f;
        float currMovenentTime = 0f;
        Vector3 dest = hit.transform.position;
        while (Vector3.Distance(transform.position, dest) > 0.0001f)
        {
            currMovenentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, dest, currMovenentTime / totalMovementTime);
            yield return null;
        }
        GetComponent<ObjectManipulator>().enabled = false;
        if (!correct)
            SuccessGoing?.Invoke();
        else
            UnSuccessGoing?.Invoke();
        isMoving = false;
    }
}
