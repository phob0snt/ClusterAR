using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance;

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
    public void ChangeGamemode(string gameMode)
    {
        StartCoroutine(OnSceneLoading(gameMode));
    }

    public static IEnumerator ExitToMainMenu(string currGM)
    {
        Debug.Log("esss");
        var unloadingScene = SceneManager.UnloadSceneAsync(currGM);
        while (!unloadingScene.isDone)
        {
            yield return null;
            Debug.Log("waitbro");
        }
    }

    private IEnumerator OnSceneLoading(string gameMode)
    {
        if (SceneManager.loadedSceneCount == 1)
        {
            switch (gameMode)
            {
                case "DropNCatch":
                    GameManager.CurrentGamemode = DNCManager.Instance;
                    break;
                case "Banger":
                    GameManager.CurrentGamemode = BangerManager.Instance;
                    break;
            }
            GameManager.CurrentGamemode.ConfigureSession();
            var loadingScene = SceneManager.LoadSceneAsync(gameMode, LoadSceneMode.Additive);
            while (!loadingScene.isDone)
            {
                yield return null;
                Debug.Log("waitbro");
            }
        }
        else
        {
            switch (gameMode)
            {
                case "DropNCatch":
                    GameManager.CurrentGamemode = DNCManager.Instance;
                    break;
                case "Banger":
                    GameManager.CurrentGamemode = BangerManager.Instance;
                    break;
            }
            GameManager.CurrentGamemode.ConfigureSession();
        }  
    }
}
