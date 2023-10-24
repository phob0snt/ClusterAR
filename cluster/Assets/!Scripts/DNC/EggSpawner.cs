using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EggSpawner : MonoBehaviour
{
    public static EggSpawner Instance;


    [SerializeField]
    [Range(0, 1)]
    private float _xSpread = 0;

    [SerializeField]
    [Range(0, 0.5f)]
    private float _ySpread = 0;

    [SerializeField]
    private float _spawnHeight = 1;

    public static bool _isSpawning = true;

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

    private Vector3 GetEggPos()
    {
        System.Random random = new System.Random();
        return new Vector3(random.Next((int)(-_xSpread * 1000), (int)(_xSpread * 1000)) * 0.001f, _spawnHeight, random.Next((int)(-_ySpread * 1000), (int)(_ySpread * 1000)) * 0.001f);
    }
    public void StartSpawningEggs(GameObject egg)
    {
        StartCoroutine(SpawnEgg(egg));
    }
    private IEnumerator SpawnEgg(GameObject egg)
    {
        yield return new WaitForSeconds(3);
        GameObject temp = Instantiate(egg);
        temp.transform.position = GetEggPos();
        if (_isSpawning)
        {
            yield return StartCoroutine(SpawnEgg(egg));
        }
        else
            yield return null;
    }
}
