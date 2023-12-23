using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BangerSkins;

public class DeathRoadManager : GameMode
{
    public static DeathRoadManager Instance;

    [SerializeField] private GameObject sceneSetButton;

    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private PlayerScript player;
    [SerializeField] private Moving moving;
    [SerializeField] private WoodProtector wp;
    [SerializeField] private StoneProtector sp;
    [SerializeField] private CloudProtector cp;
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
        sceneSetButton.SetActive(false);
        mapGenerator.Generate();
        player.sceneIsSet = true;
        moving.CanMove = true;
        wp.SetPos();
        sp.SetPos();
        cp.SetPos();
    }

    public IEnumerator RespawnPlayer()
    {
        while (player.isMoving)
            yield return null;
        GameObject tempPlayer = player.gameObject;
        GameObject newPlayer = Instantiate(DeathRoadSkins.Instance.GetPlayer(), tempPlayer.transform.position, tempPlayer.transform.rotation);
        newPlayer.transform.SetParent(tempPlayer.transform.parent, true);
        player = newPlayer.GetComponent<PlayerScript>();
        player.sceneIsSet = tempPlayer.GetComponent<PlayerScript>().sceneIsSet;
        Destroy(tempPlayer);
    }

    public override void EndSession()
    {
        StartCoroutine(CustomSceneManager.ExitToMainMenu("DeathRoad"));
        GameManager.HideMainMenu(false);
    }
}
