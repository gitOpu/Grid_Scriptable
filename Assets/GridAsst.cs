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
