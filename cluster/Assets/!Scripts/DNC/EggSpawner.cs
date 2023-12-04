using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EggSpawner : MonoBehaviour
{
    public static EggSpawner Instance;
    [SerializeField] private Material cubeActiveMat;
    [SerializeField] private Material cubeDefaultMat;
    private GameObject currentSpawnPoint;

    [SerializeField] private List<Transform> eggSpawnPoints;
    [SerializeField]
    [Range(0, 1)]
    private float _xSpread = 0;

    [SerializeField]
    private float _spawnHeight = 0.6f;

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
        currentSpawnPoint = eggSpawnPoints[Random.Range(0, eggSpawnPoints.Count - 1)].gameObject;
        return currentSpawnPoint.transform.position;
    }
    public void StartSpawningEggs()
    {
        StartCoroutine(SpawnEgg());
    }
    private IEnumerator SpawnEgg()
    {
        GameObject egg = DNCSkins.Instance.GetEgg();
        Vector3 tempPos = GetEggPos();
        currentSpawnPoint.transform.parent.GetChild(2).GetComponent<MeshRenderer>().material = cubeActiveMat;
        yield return new WaitForSeconds(1.5f);
        currentSpawnPoint.transform.parent.GetChild(2).GetComponent<MeshRenderer>().material = cubeDefaultMat;
        yield return new WaitForSeconds(1.5f);
        GameObject temp = Instantiate(egg);
        temp.transform.position = tempPos;
        if (_isSpawning)
        {
            yield return StartCoroutine(SpawnEgg());
        }
        else
            yield return null;
    }
}
