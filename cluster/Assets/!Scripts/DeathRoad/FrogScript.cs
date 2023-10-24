using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private bool isMoving;
    public static int Score;

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
            //Debug.DrawRay(transform.position, -transform.right * 0.05f);

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
                        if (hit.transform.gameObject.GetComponent<Protector>().IsCorrect)
                        {
                            StartCoroutine(Move(Directions.Forward));
                            Score += 4;
                        }
                        else
                            Death();
                        break;
                    case "AfterDanger":
                        StartCoroutine(Move(Directions.Forward));
                        break;
                }
            }
            catch { }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private IEnumerator Move(Directions dir)
    {
        Quaternion rot;
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
        if (transform.rotation.eulerAngles.y > 200)
            rot = Quaternion.Euler(-45, 0, 0);
        else if (transform.rotation.eulerAngles.y > 45)
            rot = Quaternion.Euler(45, 0, 0);
        else
            rot = Quaternion.Euler(0, 0, 45);
        float totalMovementTime = 20f;
        float currMovenentTime = 0f;
        RaycastHit hit;
        Physics.Raycast(transform.position, rot * -transform.right, out hit, 0.5f);
        
        while (Vector3.Distance(transform.position, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.075f, hit.transform.position.z)) > 0.0001f)
        {
            currMovenentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.075f, hit.transform.position.z), currMovenentTime / totalMovementTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        isMoving = false;
        Score++;
    }
    
}
