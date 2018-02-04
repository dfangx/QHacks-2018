using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour {
    public GameObject [,] tiles;

    [Header("Map Dimensions")]
    public int mapWidth = 20;
    public int mapHeight = 20;

    [Space]
    [Header("Vizualize Map")]
    public GameObject mapContainer;
    public GameObject tilePrefab;
    public Vector2 tileSize = new Vector2(32, 32);

    [Space]
    [Header("Map Sprites")]
    public Texture2D islandTexture;

    // Use this for initialization
    void Start () {
        tiles = new GameObject [mapHeight, mapWidth];
        Sprite[] sprites = Resources.LoadAll<Sprite>("RandomMapGen/" + islandTexture.name);
        int tileName = 0;
        int column = 0;
        int row = 0;
        float newX = 0;
        float newY = 0;
        int island_txt_counter = 0;
        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                newX = column * tileSize.x;
                newY = -row * tileSize.y;

                var go = Instantiate(tilePrefab);
                go.name = "Tile " + tileName;
                go.transform.SetParent(mapContainer.transform);
                go.transform.position = new Vector3(newX, newY, 0);

                var sr = go.GetComponent<SpriteRenderer>();
                sr.sprite = sprites[island_txt_counter];

                island_txt_counter++;
                if (island_txt_counter == 22)
                {
                    island_txt_counter = 0;
                }
                tileName++;
                column++;

                if (column == mapWidth)
                {
                    column = 0;
                    row++;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
