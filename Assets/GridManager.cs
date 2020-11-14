using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject containerObject;
    public GridModel[] gridModel;
   
   
    public int currentGrid;
    public Grid[,] grid;
    public float fileRate;
    void Start()
    {
        
        fileRate = 0.5f;
        GridAsst.vertical = (int)Camera.main.orthographicSize;
        GridAsst.horizontal = GridAsst.vertical * (Screen.width / Screen.height);
        GridAsst.columns = GridAsst.horizontal * 2;
        GridAsst.rows = GridAsst.vertical * 2;
        grid = new Grid[GridAsst.columns, GridAsst.rows];
        LoadGrid(currentGrid, fileRate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            fileRate = 0.3f;
           currentGrid = currentGrid < gridModel.Length - 1 ? currentGrid += 1 : 0;
            UpdateGrid(currentGrid, fileRate);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            fileRate = 0.5f;
            currentGrid = currentGrid < gridModel.Length - 1 ? currentGrid += 1 : 0;
            UpdateGrid(currentGrid, fileRate);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            fileRate = 0.7f;
            currentGrid = currentGrid < gridModel.Length - 1 ? currentGrid += 1 : 0;
            UpdateGrid(currentGrid, fileRate);
        }
    }
    void LoadGrid(int currentGrid, float fileRate)
    {
        for (int i = 0; i < GridAsst.columns; i++)
            for (int j = 0; j < GridAsst.rows; j++)
                grid[i, j] = new Grid(containerObject, gridModel[currentGrid], i, j, fileRate);
    }
    
    void UpdateGrid(int currentGrid, float fillRate)
    {
        for (int i = 0; i < GridAsst.columns; i++)
            for (int j = 0; j < GridAsst.rows; j++)
                grid[i, j].UpdateGrid(gridModel[currentGrid], fillRate);
    }
}
