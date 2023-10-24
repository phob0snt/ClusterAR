using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class ScoreMenuScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro tmp;

    private void Awake()
    {
        gameObject.GetComponent<FollowMeToggle>().AutoFollowTransformTarget = GameObject.Find("Player").transform;
    }
    void Update()
    {
        switch (GameManager.CurrentGamemode)
        {
            case BangerManager:
                tmp.text = $"Score: {BangerHead.Score}";
                break;
            case DNCManager:
                tmp.text = $"Score: {BallCounter.Score}";
                break;
            case DeathRoadManager:
                tmp.text = $"Score: {FrogScript.Score}";
                break;
        }
        
    }
}
