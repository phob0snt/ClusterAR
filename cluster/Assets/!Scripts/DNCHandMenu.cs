using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNCHandMenu : MonoBehaviour
{
    public void ExitGamemode()
    {
        Debug.Log("DNC exit");
        EggSpawner._isSpawning = false;
        StartCoroutine(CustomSceneManager.ExitToMainMenu("DropNCatch"));
        GameManager.HideMainMenu(false);
    }
}
