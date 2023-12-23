using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BangerHead : MonoBehaviour
{
    private const float HIT_STRENGTH = 0.00008f;
    private const float HEADS_MOVE_DIST = 0.1f;
    public static Action onCollision;
    private int _lerpCount = 0;
    private int _framesForLerp = 90;
    public HeadPos currHeadPos = HeadPos.Bottom;
    public static Action onHeadUp;
    public static int Score = 0;
    public static bool isRaising = false;

    public enum HeadPos
    {
        Top,
        Bottom
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            if (currHeadPos == HeadPos.Top && other.GetComponent<VelocityCheck>().PrevFrameVel > HIT_STRENGTH)
            {
                Score++;
                BangerSkins.Instance.IncreaseMoney(1);
                StartCoroutine(MoveHeadByY(-HEADS_MOVE_DIST));
            }
        }
    }
    public void RaiseHead()
    {
        StartCoroutine(MoveHeadByY(HEADS_MOVE_DIST));
    }

    private IEnumerator MoveHeadByY(float y)
    {
        while (true)
        {
            float interpRatio = (float)_lerpCount / _framesForLerp;
            Vector3 interpPos = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, transform.position.y + y,
                transform.position.z), interpRatio);
            transform.position = interpPos;
            Debug.Log(interpRatio);
            _lerpCount = (_lerpCount + 1) % (_framesForLerp + 1);
            if (_lerpCount > 10f)
            {
                _lerpCount = 0;
                _framesForLerp = 90;
                if (y > 0)
                    currHeadPos= HeadPos.Top;
                else if (y < 0)
                {
                    currHeadPos = HeadPos.Bottom;
                    if (isRaising)
                    {
                        yield return new WaitForSeconds(new System.Random().Next(1, 3));
                        onCollision?.Invoke();
                    }
                }  
                Debug.Log(currHeadPos);
                break;
            }  
            yield return null;
        }
    }
}
