using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private bool isMoving;


    enum Directions
    {
        Forward,
        Left,
        Right
    }
    void Update()
    {
        if (!isMoving)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, -transform.up, out hit, 0.05f);
            Debug.DrawRay(transform.position, -transform.right * 0.06f);
            try
            {
                switch (hit.transform.gameObject.tag)
                {
                    case "Forward":
                        StartCoroutine(Move(Directions.Forward));
                        break;
                    case "Right":
                        StartCoroutine(Move(Directions.Right));
                        break;
                    case "Left":
                        StartCoroutine(Move(Directions.Left));
                        break;
                }
            }
            catch { }
        }
    }

    private void MoveOverDanger()
    {
        StartCoroutine(Move(Directions.Forward));
    }

    private IEnumerator Move(Directions dir)
    {
        isMoving = true;
        switch (dir)
        {
            case Directions.Right:
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
                break;
            case Directions.Left:
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90, 0);
                break;
        }
        float totalMovementTime = 100f;
        float currMovenentTime = 0f;
        Vector3 dest = transform.position - transform.right * 0.1f;
        while (Vector3.Distance(transform.position, dest) > 0.04f)
        {
            currMovenentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, dest, currMovenentTime / totalMovementTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        isMoving = false;
    }
    
}
