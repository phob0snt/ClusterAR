using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DNCManager : GameMode
{
    public static DNCManager Instance;
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
        SpawnBucket(DNCSkins.Instance.GetBucket());
        EggSpawner.Instance.StartSpawningEggs(DNCSkins.Instance.GetEgg());
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
        var buck = Instantiate(bucket, new Vector3(player.x, player.y - 0.2f, player.z + 0.3f), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(buck, SceneManager.GetSceneByName("DropNCatch"));
    }
}