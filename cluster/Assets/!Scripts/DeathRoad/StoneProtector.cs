using DG.Tweening;
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
    [SerializeField] private GameObject prefab;
    private RaycastHit hit;
    private bool isMoving;
    private Vector3 startPos;

    public void SetPos()
    {
        startPos = transform.position;
    }

    void Update()
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
        transform.DORotate(Vector3.zero, 1);
        while (Vector3.Distance(transform.position, hit.transform.position) > 0.0001f)
        {
            currMovenentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, hit.transform.position, currMovenentTime / totalMovementTime);
            yield return null;
        }
        Instantiate(prefab, startPos, Quaternion.identity).transform.parent = transform.parent;
        transform.parent = hit.transform.parent;
        GetComponent<ObjectManipulator>().enabled = false;
        isMoving = false;
    }
}
