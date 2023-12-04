using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int[,] gameField = new int[16, 16];
    public Transform WallsParent;
    public GameObject WallPrefab;

    private void Awake()
    {
        GenerateLevel();
    }
    //blockpos: x = -0.42 + rowIndex * 0.06; y = 0.48 - colIndex * 0.06
    private void GenerateLevel()
    {
        for (int i = 0; i < Mathf.Sqrt(gameField.Length); i++)
            for (int j = 0; j < Mathf.Sqrt(gameField.Length); j++)
            {
                if (j == 0 || j == 15 || i == 15)
                {
                    gameField[i, j] = 1;
                    Instantiate(WallPrefab, WallsParent).transform.localPosition = new Vector3(-0.42f + j * 0.06f, 0.48f - i * 0.06f, -0.12f);
                }
            }
    }
}
public enum ObjType { Rect, WallPart, GenRoom };
public interface ILevelObject
{
    public ObjType Type { get; }
}

struct LevelRect : ILevelObject
{
    public ObjType Type => ObjType.Rect;
    public int Length;
    public int Width;
}
struct LevelWallPart : ILevelObject
{
    public ObjType Type => ObjType.WallPart;
    public int Length;
}

struct LevelGenRoom : ILevelObject
{
    public ObjType Type => ObjType.GenRoom;
    public int Length;
    public int Width;
    public int EnergyNeeded;
}
