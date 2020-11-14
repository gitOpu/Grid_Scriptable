using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Grid 
{
    public GameObject gameObject;
    public int i, j;
    public SpriteRenderer renderer;
    public GridModel gridModel;
    public Decoration decoration;
    private float fillRate;
    readonly GameObject prefab;

    public Grid(GameObject prefab, GridModel gridModel, int i, int j, float fillRate)
    {
        this.i = i;
        this.j = j;
        gameObject = GameObject.Instantiate(prefab, GridAsst.GridToWorldPosition(i, j), Quaternion.identity);
        renderer = gameObject.GetComponent<SpriteRenderer>();
        this.gridModel = gridModel;
        gameObject.name = $"i {i} j {j}";
        renderer.sprite = gridModel.gridModelSprite[GridAsst.GetGridTiles(i,j)];

        decoration = new Decoration();
        this.prefab = prefab;
        this.fillRate = fillRate;

        if(!GridAsst.IsEdgeGrid(i,j))
        if (Random.Range(0f, 1.0f) < fillRate)
            AddDecoration();
    }

   

    public void UpdateGrid(GridModel gridModel, float fillRate)
    {
        this.gridModel = gridModel;
        renderer.sprite = gridModel.gridModelSprite[GridAsst.GetGridTiles(i, j)];
        ClearDecoration();
        if (!GridAsst.IsEdgeGrid(i, j))
            if (Random.Range(0f, 1.0f) < fillRate)
                AddDecoration();
    }
    public void AddDecoration()
    {
        if (!decoration.isCreated)
       {
            int index = Random.Range(0, gridModel.decoration.Length - 1);
            Debug.Log("decoration Length " + gridModel.decoration.Length);
           decoration = new Decoration(prefab, gridModel.decoration[index], i, j, gameObject.transform);
        }
    }
    public void ClearDecoration()
    {
        GameObject.Destroy(decoration.gameObject);
        decoration = new Decoration();
    }
}

public struct Decoration
{
    public bool isCreated; 
    public GameObject gameObject;
    public SpriteRenderer renderer;
    
    public Decoration(GameObject prefab, GridDecorationModel decoration, int i, int j, Transform parent = null)
    {
        gameObject = GameObject.Instantiate(prefab, GridAsst.GridToWorldPosition(i, j), Quaternion.identity, parent);
        gameObject.name = $"X {i} Y {j}";
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = decoration.sprite;
        renderer.sortingOrder = 1;
        isCreated = true;
    }
    public void UpdateDecoration(GridModel gridModel)
    {
        if (renderer)
            renderer.sprite = gridModel.decoration[Random.Range(0, gridModel.decoration.Length)].sprite; 
    }

}
