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
            var loadingScene = SceneManager.LoadSceneAsync(gameMode, LoadSceneMode.Additive);
            while (!loadingScene.isDone)
            {
                yield return null;
                Debug.Log("waitbro");
            }
            switch (gameMode)
            {
                case "DropNCatch":
                    GameManager.CurrentGamemode = DNCManager.Instance;
                    break;
                case "Banger":
                    GameManager.CurrentGamemode = BangerManager.Instance;
                    Debug.Log(GameManager.CurrentGamemode);
                    break;
            }
            GameManager.CurrentGamemode.ConfigureSession();
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
