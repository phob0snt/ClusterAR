using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waver : MonoBehaviour
{
    public enum Dirs { Up, Right, Left, Down };
    private bool loaded;
    private void Awake()
    {
        //HandWaveController.OnWavingHand.AddListener(MoveByDir);
    }
    void Update()
    {
        if (!loaded)
        {
            if (HandWaveController.Instance != null)
                HandWaveController.OnWavingHand.AddListener(MoveByDir);
            loaded = true;
        }
        
        Debug.DrawRay(transform.position, transform.up);
        //Debug.Log(transform.forward);
    }

    public void MoveByDir(Dirs dir)
    {
        StartCoroutine(MovePlayer(dir));
    }

    private IEnumerator MovePlayer(Dirs dir)
    {
        RaycastHit hit;
        float currLerpMoment = 0f;
        float lerpTime = 50f;
        switch (dir)
        {
            case Dirs.Up:
                transform.eulerAngles = Vector3.zero;
                Physics.Raycast(transform.position, transform.up, out hit, 1);
                while (Vector3.Distance(transform.position, new Vector3(hit.transform.position.x, hit.transform.position.y - 0.06f, hit.transform.position.z)) > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(hit.transform.position.x, hit.transform.position.y - 0.06f, hit.transform.position.z), currLerpMoment);
                    currLerpMoment += Time.deltaTime;
                    if (currLerpMoment >= lerpTime)
                    {
                        break;
                    }
                    yield return null;
                }
                break;
            case Dirs.Down:
                transform.eulerAngles = new Vector3(0, 0, -180);
                Physics.Raycast(transform.position, transform.up, out hit, 1);
                Debug.Log(hit.transform.position);
                Debug.Log(Vector3.Distance(transform.position, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.06f, hit.transform.position.z)));
                if (Vector3.Distance(transform.position, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.06f, hit.transform.position.z)) > 0.05f)
                {
                    while (Vector3.Distance(transform.position, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.06f, hit.transform.position.z)) > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(hit.transform.position.x, hit.transform.position.y + 0.06f, hit.transform.position.z), currLerpMoment);
                        currLerpMoment += Time.deltaTime;
                        if (currLerpMoment >= lerpTime)
                        {
                            break;
                        }
                        yield return null;
                    }
                }
                break;
            case Dirs.Right:
                transform.eulerAngles = new Vector3(0, 0, -90);
                Physics.Raycast(transform.position, transform.up, out hit, 1);
                while (Vector3.Distance(transform.position, new Vector3(hit.transform.position.x - 0.06f, hit.transform.position.y, hit.transform.position.z)) > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(hit.transform.position.x - 0.06f, hit.transform.position.y, hit.transform.position.z), currLerpMoment);
                    currLerpMoment += Time.deltaTime;
                    if (currLerpMoment >= lerpTime)
                    {
                        break;
                    }
                    yield return null;
                }
                break;
            case Dirs.Left:
                transform.eulerAngles = new Vector3(0, 0, 90);
                Physics.Raycast(transform.position, transform.up, out hit, 1);
                while (Vector3.Distance(transform.position, new Vector3(hit.transform.position.x + 0.06f, hit.transform.position.y, hit.transform.position.z)) > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(hit.transform.position.x + 0.06f, hit.transform.position.y, hit.transform.position.z), currLerpMoment);
                    currLerpMoment += Time.deltaTime;
                    if (currLerpMoment >= lerpTime)
                    {
                        break;
                    }
                    yield return null;
                }
                break;
        }
        yield return new WaitForSeconds(1);
    }

}
