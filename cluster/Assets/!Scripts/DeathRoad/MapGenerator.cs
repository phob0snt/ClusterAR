using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> voxelPrefabs;
    [SerializeField] private Transform PlaySpace;
    // 0 - side, 1 - grass, 2 - road, 3 - RTurn, 4 - LTurn, 5 - befLavaDang, 6 - befWaterDang, 7 - befHoleDang, 
    // 8 - lavaDang, 9 - waterDang, 10 - holeDang, 11 - afterLavaDang, 12 - afterWaterDang, 13 - afterHoleDang
    private readonly Dictionary<int, GameObject> voxels = new();  
    private List<int> currLine;
    private const int LINES_ONSCENE = 20;
    private List<int> prevLine = new() { 0, 1, 1, 2, 1, 1, 1, 0};
    private Rotation prevLineRotation = Rotation.None;
    private int numOfLine = 4;

    private enum Rotation
    {
        None,
        Right,
        Left
    }
    private enum CurrRoadState
    {
        Default,
        TurnedRight,
        TurnedLeft,
        TurnedRightWDanger,
        TurnedLeftWDanger
    }
    //если ширина позволяет то делаем проверку на дангер шанс дангера 20 проц всегда
    //пока повернута будет генерить либо дорогу либо поворот в противоположном напр, если дорога стейт не меняется если поворот то стейт прямая, если ласт воксель и поворота не было то он будет поворотом
    //скан прошлую линию и напротив дороги будет создан воксель сначала через ранд чекаем будет ли это прямая поворот или бефордангер
    //сначала ключевые объекты, основанные на прошлой линии, затем наполняем травой и боковыми кусками
    private void Awake()
    {
        currLine = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0};

        for (int i = 0; i < voxelPrefabs.Count; i++)
        {
            voxels.Add(i, voxelPrefabs[i]);
        }
        Generate();
    }

    private void Update()
    {
        Debug.Log(numOfLine);
    }

    public void Generate()
    {
        GenerateLine();
    }

    private IEnumerator BuildLine(Rotation rotation = Rotation.None)
    {
        Debug.Log("PSIKA");
        for (int i = 0; i < currLine.Count; i++)
        {
            //Debug.Log(currLine[i]);
            if (i > 0 && i < 7 && currLine[i] == 0)
                currLine[i] = 1;
            GameObject voxel = Instantiate(voxelPrefabs[currLine[i]], new Vector3(transform.position.x - 0.06f * numOfLine, transform.position.y, transform.position.z + 0.06f * i), Quaternion.identity);
            voxel.transform.parent = PlaySpace;
            if (i == 0)
                voxel.transform.eulerAngles = new Vector3(0, 180, 0);
            
            switch (rotation)
            {
                case Rotation.None:
                    if ((currLine[i] > 4 && currLine[i] < 8) || (currLine[i] > 10 && currLine[i] < 14))
                        voxel.transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                case Rotation.Right:
                    if (currLine[i] == 2)
                        voxel.transform.eulerAngles = new Vector3(0, 90, 0);
                    else if (currLine[i] == 3)
                        voxel.transform.eulerAngles = new Vector3(0, 180, 0);
                    else if (currLine[i] == 4)
                        voxel.transform.eulerAngles = new Vector3(0, -90, 0);
                    else if ((currLine[i] > 4 && currLine[i] < 8) || (currLine[i] > 10 && currLine[i] < 14))
                        voxel.transform.eulerAngles = new Vector3(0, -90, 0);
                    break;
                case Rotation.Left:
                    if (currLine[i] == 2)
                        voxel.transform.eulerAngles = new Vector3(0, -90, 0);
                    else if (currLine[i] == 3)
                        voxel.transform.eulerAngles = new Vector3(0, 90, 0);
                    else if (currLine[i] == 4)
                        voxel.transform.eulerAngles = new Vector3(0, 180, 0);
                    else if ((currLine[i] > 4 && currLine[i] < 8) || (currLine[i] > 10 && currLine[i] < 14))
                        voxel.transform.eulerAngles = new Vector3(0, 90, 0);
                    break;
            }
            
        }
        prevLine = currLine;
        currLine = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
        if (numOfLine < LINES_ONSCENE)
        {
            yield return null;
            numOfLine++;
            GenerateLine();
        }
        else
        {
            numOfLine++;
        }
            
    }

    private void GenerateLine(CurrRoadState state = CurrRoadState.Default)
    {
        //isGenerating = true;
        Debug.Log("PREV LINE:");
        foreach (int i in prevLine)
        {
            Debug.Log(i);
        }
        //    Debug.Log("CURR LINE:");
        //    foreach (int i in currLine)
        //    {
        //        Debug.Log(i);
        //    }
        int roadIndex = 0;
        if (prevLineRotation == Rotation.Right)
            roadIndex = prevLine.FindLastIndex(x => x == 4);
        else if (prevLineRotation == Rotation.Left)
            roadIndex = prevLine.FindIndex(x => x == 3);
        else
        {
            if (!(prevLine.FindIndex(x => x > 7) == -1))
            {
                roadIndex = prevLine.FindIndex(x => x > 7);
            }
            else if (!(prevLine.FindIndex(x => x > 4) == -1))
                roadIndex = prevLine.FindIndex(x => x > 4);
            else if (prevLine.FindIndex(x => x > 2) == -1)
                roadIndex = prevLine.FindIndex(x => x > 1);
        }
                switch (state)
        {
            case CurrRoadState.Default:
                Debug.Log(roadIndex);
                if ((prevLine[roadIndex] >= 5 && prevLine[roadIndex] <= 7) || (prevLine[roadIndex] >= 8 && prevLine[roadIndex] <= 10))
                {
                    Debug.Log("DA");
                    currLine[roadIndex] = prevLine[roadIndex] + 3;
                    Debug.Log(currLine[roadIndex]);
                    StartCoroutine(BuildLine());
                    prevLineRotation = Rotation.None;
                    return;
                }
                else if (roadIndex == 6)
                {
                    int randVox = UnityEngine.Random.Range(0, 4);
                    if (randVox < 3)
                        randVox = 0;
                    else
                        randVox = 1;
                    switch (randVox)
                    {
                        case 0:
                            int voxelNum = UnityEngine.Random.Range(2, 5);
                            if (voxelNum != 4)
                                voxelNum = 2;
                            currLine[roadIndex] = voxelNum;
                            switch (voxelNum)
                            {
                                case 2:
                                    StartCoroutine(BuildLine());
                                    prevLineRotation = Rotation.None;
                                    return;
                                case 4:
                                    prevLineRotation = Rotation.Left;
                                    GenerateLine(CurrRoadState.TurnedLeft);
                                    return;
                            }
                            break;
                        case 1:
                            currLine[roadIndex] = UnityEngine.Random.Range(5, 8);
                            StartCoroutine(BuildLine());
                            prevLineRotation = Rotation.None;
                            return;
                    }
                }
                else if (roadIndex == 1)
                {
                    int randVox = UnityEngine.Random.Range(0, 4);
                    if (randVox < 3)
                        randVox = 0;
                    else
                        randVox = 1;
                    switch (randVox)
                    {
                        case 0:
                            int voxelInd = UnityEngine.Random.Range(2, 4);
                            currLine[roadIndex] = voxelInd;
                            switch (voxelInd)
                            {
                                case 2:
                                    StartCoroutine(BuildLine());
                                    prevLineRotation = Rotation.None;
                                    return;
                                case 3:
                                    prevLineRotation = Rotation.Right;
                                    GenerateLine(CurrRoadState.TurnedRight);
                                    return;
                            }
                            break;
                        case 1:
                            currLine[roadIndex] = UnityEngine.Random.Range(5, 8);
                            StartCoroutine(BuildLine());
                            prevLineRotation = Rotation.None;
                            return;
                    }
                }
                else
                {
                    int randVox = UnityEngine.Random.Range(0, 4);
                    if (randVox < 3)
                        randVox = 0;
                    else
                        randVox = 1;
                    switch (randVox)
                    {
                        case 0:
                            int voxelInd = UnityEngine.Random.Range(2, 5);
                            currLine[roadIndex] = voxelInd;
                            switch (voxelInd)
                            {
                                case 2:
                                    StartCoroutine(BuildLine());
                                    prevLineRotation = Rotation.None;
                                    return;
                                case 3:
                                    prevLineRotation = Rotation.Right;
                                    GenerateLine(CurrRoadState.TurnedRight);
                                    return;
                                case 4:
                                    prevLineRotation = Rotation.Left;
                                    GenerateLine(CurrRoadState.TurnedLeft);
                                    return;
                            }
                            break;
                        case 1:
                            currLine[roadIndex] = UnityEngine.Random.Range(5, 8);
                            StartCoroutine(BuildLine());
                            prevLineRotation = Rotation.None;
                            return;
                    }
                }
                break;
            case CurrRoadState.TurnedRight:
                {
                    int lastVoxelInd = currLine.FindLastIndex(x => x >= 2);
                    if (lastVoxelInd <= 2)
                    {
                        switch (UnityEngine.Random.Range(0, 1))
                        {
                            case 0:
                                currLine[lastVoxelInd + 1] = UnityEngine.Random.Range(5, 8);
                                GenerateLine(CurrRoadState.TurnedRightWDanger);
                                return;
                            case 1:
                                switch (UnityEngine.Random.Range(0, 2))
                                {
                                    case 0:
                                        currLine[lastVoxelInd + 1] = 2;
                                        GenerateLine(CurrRoadState.TurnedRight);
                                        return;
                                    case 1:
                                        currLine[lastVoxelInd + 1] = 4;
                                        StartCoroutine(BuildLine(Rotation.Right));
                                        return;
                                }
                                break;
                        }
                    }
                    else if (lastVoxelInd > 2 && lastVoxelInd < 4)
                    {
                        switch (UnityEngine.Random.Range(0, 2))
                        {
                            case 0:
                                currLine[lastVoxelInd + 1] = 2;
                                GenerateLine(CurrRoadState.TurnedRight);
                                return;
                            case 1:
                                currLine[lastVoxelInd + 1] = 4;
                                StartCoroutine(BuildLine(Rotation.Right));
                                return;
                        }
                        break;
                    }
                    else
                    {
                        currLine[lastVoxelInd + 1] = 4;
                        StartCoroutine(BuildLine(Rotation.Right));
                        return;
                    }
                    break;
                }
            case CurrRoadState.TurnedLeft:
                {
                    int lastVoxelInd = currLine.FindIndex(x => x >= 2);
                    if (lastVoxelInd >= 6)
                    {
                        switch (UnityEngine.Random.Range(1, 1))
                        {
                            case 0:
                                currLine[lastVoxelInd - 1] = UnityEngine.Random.Range(5, 8);
                                GenerateLine(CurrRoadState.TurnedLeftWDanger);
                                return;
                            case 1:
                                switch (UnityEngine.Random.Range(0, 2))
                                {
                                    case 0:
                                        currLine[lastVoxelInd - 1] = 2;
                                        GenerateLine(CurrRoadState.TurnedLeft);
                                        return;
                                    case 1:
                                        currLine[lastVoxelInd - 1] = 3;
                                        StartCoroutine(BuildLine(Rotation.Left));
                                        return;
                                }
                                break;
                        }
                    }
                    else if (lastVoxelInd < 5 && lastVoxelInd > 2)
                    {
                        switch (UnityEngine.Random.Range(0, 2))
                        {
                            case 0:
                                currLine[lastVoxelInd - 1] = 2;
                                GenerateLine(CurrRoadState.TurnedLeft);
                                return;
                            case 1:
                                currLine[lastVoxelInd - 1] = 3;
                                StartCoroutine(BuildLine(Rotation.Left));
                                return;
                        }
                        break;
                    }
                    else
                    {
                        currLine[lastVoxelInd - 1] = 3;
                        StartCoroutine(BuildLine(Rotation.Left));
                        return;
                    }
                    break;
                }
            case CurrRoadState.TurnedRightWDanger:
                foreach (int i in currLine)
                {
                    Debug.Log("@@@@" + i);
                }
                int lastVoxInd = currLine.FindLastIndex(x => x >= 5);
                if (currLine[lastVoxInd] > 10)
                    GenerateLine(CurrRoadState.TurnedRight);
                else
                {
                    currLine[lastVoxInd + 1] = currLine[lastVoxInd] + 3;
                    GenerateLine(CurrRoadState.TurnedRightWDanger);
                }
                break;
            case CurrRoadState.TurnedLeftWDanger:
                break;
        }
        
    }
}
