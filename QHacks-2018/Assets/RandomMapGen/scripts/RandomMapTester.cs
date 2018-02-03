using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapTester : MonoBehaviour {
    [Header("Map Dimensions")]
    public int mapWidth = 20;
    public int mapHeight = 20;

    [Space]
    [Header("Visualize Map")]
    public GameObject mapContainer;
    public GameObject tilePrefab;
    public Vector2 tileSize = new Vector2(16, 16);

    [Space]
    [Header("Map Sprites")]
    public Texture2D islandTexture;

    public Map map;

    private void Start()
    {
        map = new Map();
    }

    public void MakeMap()
    {
        map.NewMap(mapWidth, mapHeight);
        Debug.Log("Created a new " + map.columns + "x" + map.rows + " map");
        CreateGrid();
    }

    void CreateGrid()
    {
        ClearMapContainer();
        Sprite[] sprites = Resources.LoadAll <Sprite>(islandTexture.name);   

        var total = map.tiles.Length;
        var maxColumns = map.columns;
        var column = 0;
        var row = 0;

        for (var i = 0; i < total; i++)
        {
            column = i % maxColumns;

            var newX = column * tileSize.x;
            var newY = -row * tileSize.y;

            var go = Instantiate(tilePrefab);
            go.name = "Tile " + i;
            go.transform.SetParent(mapContainer.transform);
            go.transform.position = new Vector3(newX, newY, 0);

            var spriteID = 0;
            var sr = go.GetComponent<SpriteRenderer>();
            sr.sprite = sprites[spriteID];

            if (column == (maxColumns - 1))
                row++;
        }
    }

    void ClearMapContainer()
    {
        var children = mapContainer.transform.GetComponentsInChildren<Transform>();

        for (var i = children.Length - 1; i > 0; i--)
            Destroy(children[i].gameObject);
    }
}
