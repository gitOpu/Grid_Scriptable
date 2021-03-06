﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create New Grid", menuName = "Grid Model/New Model")]
public class GridModel : ScriptableObject
{
    public Sprite[] gridModelSprite;
   public GridDecorationModel[] decoration;
}
