using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private bool isMoving;
    private bool canGoOverDanger;

    private void OnEnable()
    {
        Protector.SuccessGoing += WillMove;
        Protector.UnSuccessGoing += WontMove;
    }

    private void OnDisable()
    {
        Protector.SuccessGoing -= WillMove;
        Protector.UnSuccessGoing -= WontMove;
    }
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
            Debug.DrawRay(transform.position, -transform.right * 0.05f);
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
                    case "BeforeDanger":
                        if (hit.transform.gameObject.GetComponent<ProtectorState>().isProtected)
                            StartCoroutine(Move(Directions.Forward));
                        break;
                    case "Protector":
                        if (canGoOverDanger)
                        {
                            StartCoroutine(Move(Directions.Forward));
                            canGoOverDanger = false;
                        }
                        else
                            Death();
                        break;
                }
            }
            catch { }
        }
    }

    private void WillMove()
    {
        canGoOverDanger = true;
    }

    private void WontMove()
    {
        canGoOverDanger = false;
    }
    private void Death()
    {
        Destroy(gameObject);
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
        float totalMovementTime = 70f;
        float currMovenentTime = 0f;
        Vector3 dest = transform.position - transform.right * 0.06f;
        while (Vector3.Distance(transform.position, dest) > 0.0001f)
        {
            currMovenentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, dest, currMovenentTime / totalMovementTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        Debug.Log("end");
        isMoving = false;
    }
    
}
