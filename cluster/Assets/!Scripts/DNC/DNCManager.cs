using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DNCManager : GameMode
{
    public static DNCManager Instance;
    [SerializeField] private Transform bucketPos;
    [SerializeField] private Transform sceneCollider;
    [SerializeField] private GameObject sceneSetButton;

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

    public override void ConfigureSession()
    {
        GameManager.HideMainMenu(true);
    }

    public void StartSession()
    {
        SpawnBucket(DNCSkins.Instance.GetBucket());
        EggSpawner.Instance.StartSpawningEggs();
        sceneSetButton.SetActive(false);
        
    }

    public override void EndSession()
    {
        EggSpawner._isSpawning = false;
        StartCoroutine(CustomSceneManager.ExitToMainMenu("DropNCatch"));
        GameManager.HideMainMenu(false);
    }
    private void SpawnBucket(GameObject bucket)
    {
        Vector3 player = GameObject.Find("Player").transform.position;
        var buck = Instantiate(bucket, bucketPos.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(buck, SceneManager.GetSceneByName("DropNCatch"));
        buck.transform.SetParent(sceneCollider);
    }
}