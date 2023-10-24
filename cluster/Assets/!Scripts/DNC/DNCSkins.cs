using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNCSkins : MonoBehaviour
{
    public static DNCSkins Instance;
    private int _DNCMoney;

    [SerializeField] private List<GameObject> Buckets;
    [SerializeField] private List<GameObject> Eggs;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject GetBucket()
    {
        return Buckets[0];
    }

    public GameObject GetEgg()
    {
        return Eggs[0];
    }
}
