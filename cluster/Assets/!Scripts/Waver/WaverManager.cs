using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaverManager : GameMode
{
    public static WaverManager Instance;
    [SerializeField] private GameObject sceneSetButton;
    [SerializeField] private GameObject[] bonusSpheres;
    [SerializeField] private Transform[] spheresPoses;
    private bool[] spheresSet = { false, false, false };
    [SerializeField] private Transform coinParent;
    [SerializeField] private Transform bonusParent;
    [SerializeField] private Transform playerParent;
    public bool CanWave;
    [SerializeField] private Transform startSquare;
    private Vector3 startPos;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject doubleBonusPrefab;
    [SerializeField] private GameObject waver;

    private string[,] wallMap = new string[14, 14]
    {
        { "", "", ".", "", "", "", "", "", "", "", ".", "", ".", "." },
        { "", "", ".", "", "", ".", "", "", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "", ".", "", "", "", "", ".", "" },
        { "", ".", "", "", ".", "", "", ".", "", ".", "", "", "", "" },
        { "", "", "", "", ".", "", "", ".", ".", ".", ".", ".", ".", "." },
        { "", "", "", ".", ".", "", "", ".", "", "", "", ".", "", "" },
        { ".", ".", ".", ".", ".", "", "", ".", "", "", "", ".", "", "" },
        { ".", "", "", "", ".", "", "", ".", "", "", "", "", "", "" },
        { "", "", ".", "", ".", "", "", ".", "", ".", "", "", "", "." },
        { "", "", "", "", ".", "", "", "", "", "", "", "", "", "" },
        { "", ".", "", ".", ".", ".", ".", ".", "", "", ".", "", "", "" },
        { "", ".", "", "", "", "", "", "", "", "", ".", "", "", "" },
        { "", "", "", "", "", "", "", "", "", ".", ".", "", ".", "." },
        { "", "", "", "", "", ".", ".", ".", ".", ".", ".", "", "", "" }
    };

    public bool DoubleCoinsActive;

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
        CanWave = true;
        startPos = new Vector3(startSquare.position.x + 0.06f, startSquare.position.y - 0.06f, startSquare.position.z);
        SpawnCoin();
        StartCoroutine(SpawnBonus());
    }

    private IEnumerator SpawnBonus()
    {
        if (Random.Range(0, 16) == 10)
        {
            int row = Random.Range(0, 14);
            int column = Random.Range(0, 14);
            if (wallMap[row, column] != ".")
            {
                GameObject bonus = Instantiate(doubleBonusPrefab, new Vector3(startPos.x + (column * 0.06f), startPos.y - (row * 0.06f), startPos.z), Quaternion.identity);
                SceneManager.MoveGameObjectToScene(bonus, SceneManager.GetSceneByName("Waver"));
                bonus.transform.SetParent(bonusParent, true);
            }
        }
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(SpawnBonus());

    }

    public void RespawnWaver()
    {
        Vector3 waverPos = waver.transform.position;
        Destroy(waver);
        waver = Instantiate(WaverSkins.Instance.GetWaver(), waverPos, Quaternion.identity);
        waver.transform.SetParent(playerParent, true);
    }

    public void SpawnCoin()
    {
        int row = Random.Range(0, 14);
        int column = Random.Range(0, 14);
        if (wallMap[row, column] != ".")
        {
            GameObject coin = Instantiate(coinPrefab, new Vector3(startPos.x + (column * 0.06f), startPos.y - (row * 0.06f), startPos.z), Quaternion.identity);
            SceneManager.MoveGameObjectToScene(coin, SceneManager.GetSceneByName("Waver"));
            coin.transform.SetParent(coinParent, true);
        }
        else
            SpawnCoin();
    }

    public override void EndSession()
    {
        StartCoroutine(CustomSceneManager.ExitToMainMenu("Waver"));
        GameManager.HideMainMenu(false);
    }
    
    public void DoubleCoins()
    {
        DoubleCoinsActive = true;
        StartCoroutine(BonusTimeWaiter(15, BonusSphere.BonusType.Double));
    }

    private IEnumerator BonusTimeWaiter(int seconds, BonusSphere.BonusType type)
    {
        yield return new WaitForSeconds(seconds);
        switch (type)
        {
            case BonusSphere.BonusType.Double:
                DoubleCoinsActive = false;
                break;
        }
    }

    public void SpawnSphere(BonusSphere.BonusType bonusType)
    {
        for (int i = 0; i < spheresSet.Length; i++)
        {
            if (!spheresSet[i])
            {
                Instantiate(bonusSpheres[(int)bonusType], spheresPoses[i]);
                spheresSet[i] = true;
                return;
            }
        }
    }
}
