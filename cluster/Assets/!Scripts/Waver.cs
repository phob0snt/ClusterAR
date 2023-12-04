using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using DG.Tweening;

public class Waver : MonoBehaviour
{
    public enum Dirs { Up, Right, Left, Down };
    public static bool IsMoving;
    private void Awake()
    {
        HandWaveController.OnWavingHand.AddListener(MoveByDir);
        DOTween.Init();
    }
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up);
    }

    public void MoveByDir(Dirs dir)
    {
        StartCoroutine(MovePlayer(dir));
    }

    private IEnumerator MovePlayer(Dirs dir)
    {
        IsMoving = true;
        RaycastHit hit;
        switch (dir)
        {
            case Dirs.Up:
                transform.eulerAngles = Vector3.zero;
                Physics.Raycast(transform.position, transform.up, out hit, 10);
                transform.DOMove(new Vector3(hit.transform.position.x, hit.transform.position.y - 0.06f, hit.transform.position.z), 0.2f).SetEase(Ease.InSine);
                while (DOTween.IsTweening(transform))
                {
                    yield return null;
                }
                break;

            case Dirs.Down:
                transform.eulerAngles = new Vector3(0, 0, -180);
                Physics.Raycast(transform.position, transform.up, out hit, 10);
                transform.DOMove(new Vector3(hit.transform.position.x, hit.transform.position.y + 0.06f, hit.transform.position.z), 0.2f).SetEase(Ease.InSine);
                while (DOTween.IsTweening(transform))
                {
                    yield return null;
                }
                break;
            case Dirs.Right:
                transform.eulerAngles = new Vector3(0, 0, -90);
                Physics.Raycast(transform.position, transform.up, out hit, 10);
                transform.DOMove(new Vector3(hit.transform.position.x - 0.06f, hit.transform.position.y, hit.transform.position.z), 0.2f).SetEase(Ease.InSine);
                while (DOTween.IsTweening(transform))
                {
                    yield return null;
                }
                break;
            case Dirs.Left:
                transform.eulerAngles = new Vector3(0, 0, 90);
                Physics.Raycast(transform.position, transform.up, out hit, 10);
                transform.DOMove(new Vector3(hit.transform.position.x + 0.06f, hit.transform.position.y, hit.transform.position.z), 0.2f).SetEase(Ease.InSine);
                while (DOTween.IsTweening(transform))
                {
                    yield return null;
                }
                break;
        }
        IsMoving = false;
    }

}
