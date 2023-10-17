using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class WoodProtector : Protector
{
    public override ProtectorType type => ProtectorType.Wood;

    private RaycastHit hit;
    private bool isMoving;

    private void Update()
    {
        Debug.DrawRay(transform.localPosition, -transform.up * 0.083f);
        if (!isMoving)
        {
            Physics.Raycast(transform.position, -transform.up, out hit, 0.083f);
            try
            {
                if (hit.transform.gameObject.CompareTag(ProtectorPairs.GetValueOrDefault(type)))
                {
                    StartCoroutine(ApplyProtector());
                    IsCorrect = true;
                }
                else if (ProtectorPairs.Values.Contains(hit.transform.gameObject.tag))
                {
                    StartCoroutine(ApplyProtector());
                    IsCorrect = false;
                }
            }
            catch { }
        }
    }

    public override IEnumerator ApplyProtector()
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
        isMoving = false;
    }
}

