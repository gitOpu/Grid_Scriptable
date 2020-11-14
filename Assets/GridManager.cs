using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject containerObject;
    public GridModel[] model;
    public int currentGrid;
    private Grid[,] grid;
    
    void Start()
    {

        GridAsst.vertical = (int)Camera.main.orthographicSize;
        GridAsst.horizontal = GridAsst.vertical * (Screen.width / Screen.height);
        GridAsst.columns = GridAsst.horizontal * 2;
        GridAsst.rows = GridAsst.vertical * 2;
        grid = new Grid[GridAsst.columns, GridAsst.rows];

        LoadGrid();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           currentGrid = currentGrid < model.Length - 1 ? currentGrid += 1 : 0;
            LoadGrid();
        }
    }

    void LoadGrid()
    {
        for (int i = 0; i < GridAsst.columns; i++)
            for (int j = 0; j < GridAsst.rows; j++)
                grid[i, j] = new Grid(containerObject, model[currentGrid].gridModelSprite[GridAsst.GetGridTiles(i, j)], i, j);
    }

}
