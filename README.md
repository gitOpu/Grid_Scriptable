
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

** GridAsset.cs //  Propertise and Method
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