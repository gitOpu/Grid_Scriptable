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
