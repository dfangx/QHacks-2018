 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    public Tile[] tiles;
    public int columns;
    public int rows;

    public void NewMap(int width, int height)
    {
        columns = width;
        rows = height;

        tiles = new Tile[columns * rows];

        CreateTiles();
    }

    private void CreateTiles()
    {
        var total = tiles.Length;
        for (var i = 0; i < total; i++)
        {
            var tile = new Tile();
            tile.id = i;
            tiles[i] = tile;
        }
    }
}
