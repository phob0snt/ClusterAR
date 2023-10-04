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
        tmp.text = $"Score: {BallCounter.Score}";
    }
}
