<<<<<<< HEAD
﻿using System.Collections;
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
                sr.sprite = sprites[Random.Range(0, 21)];

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
=======
<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour {
<<<<<<< HEAD
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
=======
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
>>>>>>> 960f1663df104e7016e276a149323540fa71dc6e
    void Start () {
        tiles = new GameObject [mapHeight, mapWidth];
        Sprite[] sprites = Resources.LoadAll<Sprite>("RandomMapGen/" + islandTexture.name);
        int tileName = 0;
        int column = 0;
        int row = 0;
<<<<<<< HEAD
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
=======
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
>>>>>>> 960f1663df104e7016e276a149323540fa71dc6e
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
=======
﻿using System.Collections;
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
	
    void RandomizeIndex()
    {

    }
	// Update is called once per frame
	void Update () {
		
	}
}
>>>>>>> 0fc663b932a98cf550ff2096455a5bee0debd3af
>>>>>>> db22809c677c1ea35ad0fbf2a4beb87469d1c2fa
