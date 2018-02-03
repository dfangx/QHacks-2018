using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Sides
{
    Bottom,
    Right,
    Left,
    Top
}
public class Tile {

    public int id = 0;
    public Tile[] neighbors = new Tile[4];
}
