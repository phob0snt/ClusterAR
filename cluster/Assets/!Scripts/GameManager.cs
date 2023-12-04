using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameMode CurrentGamemode;
    [SerializeField] private GameObject _mainMenu;
    public Transform playerPos;

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

    public static void HideMainMenu(bool hide)
    {
        if (hide)
        {
            Instance._mainMenu.SetActive(false);
        }
        else
        {
            Instance._mainMenu.SetActive(true);
        }
    }



}
