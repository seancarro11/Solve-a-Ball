using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSizeX;
    public int gridSizeY;
    public float cellSize = 1.0f;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, 0, y * cellSize);
                // Instantiate your grid cell prefab or perform other actions here
            }
        }
    }
}
