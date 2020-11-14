# Project 1
Create an empty Object named it GridManager, add GridManager.cs script file to it, create new Sprite on hierchy, add Rectangle Sprite to project file, attach Box Sprite to Sprite Renderer of Sprite.

![Cover Photo](doc/1.PNG)

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public new GameObject gameObject;
     SpriteRenderer spriteRenderer;
    int columns, rows, vertical, horizontal;
    void Start()
    {
        vertical = (int)Camera.main.orthographicSize;
        horizontal = vertical * (Screen.width / Screen.height);
        columns = horizontal * 2;
        rows = vertical * 2;

        for (int i = 0; i < columns; i++)
            for (int j = 0; j < rows; j++)
                InstantiateGrid( i, j);

    }
    void InstantiateGrid(int i, int j)
    {
        var go = Instantiate(gameObject, GridToWorldPosition(i, j), Quaternion.identity);
        spriteRenderer = go.GetComponent<SpriteRenderer>();
       spriteRenderer.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        go.name =$"i {i} j {j}";
    }
    Vector3 GridToWorldPosition(int i, int j)
    {
        return new Vector3(i - (horizontal - 0.5f), j - (vertical - 0.5f));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

```
# Project 2
![Cover Photo](doc/2.PNG)
** GridModel.cs // Model
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create New Grid", menuName = "Grid Model/Create New")]
public class GridModel : ScriptableObject
{
    public Sprite[] gridModelSprite;
}
```

** GridAsset.cs //  Properties and Method
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridAsst 
{
   public static int columns, rows, vertical, horizontal;
     
       public static Vector3 GridToWorldPosition(int i, int j)
    {
        return new Vector3(i - (GridAsst.horizontal - 0.5f), j - (GridAsst.vertical - 0.5f));
    }

    public static int GetGridTiles(int i, int j)
    {
        // Upper rows
        if (i == 0 && j == rows - 1)
            return 0;
        if (i == columns - 1 && j == rows - 1)
            return 2;
        if (i != 0 && i != columns - 1 && j == rows - 1)
            return 1;
        // Lower rows
        if (i == 0 && j == 0)
            return 6;
        if (i == columns - 1 && j == 0)
            return 8;
        if (i != 0 && i != columns - 1 && j == 0)
            return 7;
        // Left rows
        if (i == 0 && j != 0 && j != rows - 1)
            return 3;
        // Right rows
        if (i == columns - 1 && j != 0 && j != rows - 1)
            return 5;
        // Middle cell
        else
            return 4;
    }

}

```

** Grid.cs // Grid View
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Grid 
{
    private GameObject gameObject;
    private int i, j;
    private SpriteRenderer renderer;

    public Grid(GameObject prefab, Sprite sprite, int i, int j)
    {
        this.i = i;
        this.j = j;
        gameObject = GameObject.Instantiate(prefab, GridAsst.GridToWorldPosition(i, j), Quaternion.identity);
        renderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.name = $"i {i} j {j}";
        renderer.sprite = sprite;
    }
}

```

** GridManager.cs // Grid Controller
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject containerObject;
    public GridModel[] model;
    public int currentGrid = 0;
    private Grid[,] grid;
    
    void Start()
    {

        GridAsst.vertical = (int)Camera.main.orthographicSize;
        GridAsst.horizontal = GridAsst.vertical * (Screen.width / Screen.height);
        GridAsst.columns = GridAsst.horizontal * 2;
        GridAsst.rows = GridAsst.vertical * 2;
        grid = new Grid[GridAsst.columns, GridAsst.rows];

        for (int i = 0; i < GridAsst.columns; i++)
            for (int j = 0; j < GridAsst.rows; j++)
                grid[i, j] = new Grid(containerObject, model[currentGrid].gridModelSprite[GridAsst.GetGridTiles(i,j)], i, j);

    }
    
    
    
}

```


# Project 3
![Cover Photo](doc/3.PNG)

** GridManager.cs // Main Controller
```C#
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

```

** Grid.cs // Model (or View Model)
```C#
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

```
** GridModel.cs // DataModel
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create New Grid", menuName = "Grid Model/New Model")]
public class GridModel : ScriptableObject
{
    public Sprite[] gridModelSprite;
   public GridDecorationModel[] decoration;
}

```

** GridDecorationModel.cs // DataModel
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Decoration", menuName = "Grid Model/New Decoration Item")]
public class GridDecorationModel : ScriptableObject
{
    public Sprite sprite;
    public float height;
}

```

** GridAsst.cs // Assistant Properties and Method
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridAsst 
{
   public static int columns, rows, vertical, horizontal;
     
       public static Vector3 GridToWorldPosition(int i, int j)
    {
        return new Vector3(i - (GridAsst.horizontal - 0.5f), j - (GridAsst.vertical - 0.5f));
    }

    public static int GetGridTiles(int i, int j)
    {
        // Upper rows
        if (i == 0 && j == rows - 1)
            return 0;
        if (i == columns - 1 && j == rows - 1)
            return 2;
        if (i != 0 && i != columns - 1 && j == rows - 1)
            return 1;
        // Lower rows
        if (i == 0 && j == 0)
            return 6;
        if (i == columns - 1 && j == 0)
            return 8;
        if (i != 0 && i != columns - 1 && j == 0)
            return 7;
        // Left rows
        if (i == 0 && j != 0 && j != rows - 1)
            return 3;
        // Right rows
        if (i == columns - 1 && j != 0 && j != rows - 1)
            return 5;
        // Middle cell
        else
            return 4;
    }

    public static bool IsEdgeGrid(int i, int j)
    {
        // Upper rows
        if (i == 0 && j == rows - 1)
            return true;
        if (i == columns - 1 && j == rows - 1)
            return true;
        if (i != 0 && i != columns - 1 && j == rows - 1)
            return true;
        // Lower rows
        if (i == 0 && j == 0)
            return true;
        if (i == columns - 1 && j == 0)
            return true;
        if (i != 0 && i != columns - 1 && j == 0)
            return true;
        // Left rows
        if (i == 0 && j != 0 && j != rows - 1)
            return true;
        // Right rows
        if (i == columns - 1 && j != 0 && j != rows - 1)
            return true;
        // Middle cell
        else
            return false;
    }

}

```
