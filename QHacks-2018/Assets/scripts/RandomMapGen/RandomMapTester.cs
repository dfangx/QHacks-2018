using UnityEngine;
using System.Collections;
using System;

public class RandomMapTester : MonoBehaviour
{

    [Header("Map Dimensions")]
    public int mapWidth = 20;
    public int mapHeight = 20;

    [Space]
    [Header("Vizualize Map")]
    public GameObject mapContainer;
    public GameObject tilePrefab;
    public Vector2 tileSize = new Vector2(16, 16);

    [Space]
    [Header("Map Sprites")]
    public Texture2D islandTexture;

    [Space]
    [Header("Decorate Map")]
    [Range(0, .9f)]
    public float erodePercent = .5f;
    public int erodeIterations = 2;
    [Range(0, .9f)]
    public float treePercent = .3f;
    [Range(0, .9f)]
    public float hillPercent = .2f;
    [Range(0, .9f)]
    public float mountainsPercent = .1f;
    [Range(0, .9f)]
    public float townPercent = .05f;
    [Range(0, .9f)]
    public float monsterPercent = .1f;
    [Range(0, .9f)]
    public float lakePercent = .05f;

    public Map map;
    public GameObject person;
    public GameObject [] zombie;
    // Use this for initialization
    void Start()
    {
        map = new Map();

        person = GameObject.Find("Person");
        zombie = GameObject.FindGameObjectsWithTag("Zombie");
        person.SetActive(false);

        MakeMap();
    }

    public void MakeMap()
    {
        map.NewMap(mapWidth, mapHeight);
        map.CreateIsland(
            erodePercent,
            erodeIterations,
            treePercent,
            hillPercent,
            mountainsPercent,
            townPercent,
            monsterPercent,
            lakePercent
        );
        CreateGrid();
        CenterMap(map.castleTile.id);
        Spawn(map.castleTile.id);
        SpawnZombie(map.castleTile.id);
    }

    void Spawn(int index)
    {
 

        var personSpawn = person.transform.position;
        var width = map.columns;
        personSpawn.x = (index % width) * tileSize.x;
        personSpawn.y = -((index / width) * tileSize.y);
        person.transform.position = personSpawn;

        person.SetActive(true);
    }
    void SpawnZombie(int index)
    {
        for (int i = 0; i < zombie.Length; i++)
        {
            var zombieSpawn = zombie[i].transform.position;
            var width = map.columns + i;
            zombieSpawn.x = (index % width) * tileSize.x;
            zombieSpawn.y = -((index / width) * tileSize.y);
            zombie[i].transform.position = zombieSpawn;
        }
        
        
        
        
        

        
    }
    void CreateGrid()
    {
        ClearMapContainer();
        Sprite[] sprites = Resources.LoadAll<Sprite>("RandomMapGen/" + islandTexture.name);

        var map_columns = map.columns + 2;
        var map_rows = map.rows + 2;
        var counter_columns = 0;
        var counter_rows = 0;
        var i = 0;

        for (counter_rows = 2; counter_rows < map_rows; counter_rows++)
        {
            for (counter_columns = 2; counter_columns < map_columns; counter_columns++)
            {
                var go = Instantiate(tilePrefab);
                var newX = counter_columns * tileSize.x;
                var newY = -counter_rows * tileSize.y;
                go.name = "Tile" + i;
                go.transform.SetParent(mapContainer.transform);
                go.transform.position = new Vector3(newX, newY, 0);
                var tile = map.tiles[i];
                var spriteID = tile.autotileID;
                i++;
                if (spriteID >= 0)
                {
                    var sr = go.GetComponent<SpriteRenderer>();
                    sr.sprite = sprites[spriteID];
                }
                else
                {
                    var sr = go.GetComponent<SpriteRenderer>();
                    go.AddComponent<BoxCollider2D>().size = new Vector2(10f, 10f); ;
                    sr.enabled = false;
                }
            }
        }
        map_columns = map.columns + 4;
        map_rows = map.rows + 4;
        counter_columns = 0;
        counter_rows = 0;

        for (counter_rows = 0; counter_rows < map_rows; counter_rows++)
        {
            for (counter_columns = 0; counter_columns < map_columns; counter_columns++)
            {
                var newX = counter_columns * tileSize.x;
                var newY = -counter_rows * tileSize.y;
                if (counter_rows == 0 || counter_rows == 1 || counter_rows == map_rows - 1 || counter_rows == map_rows - 2 || counter_columns == 0 || counter_columns == 1 || counter_columns == map_columns - 1 || counter_columns == map_columns - 2)
                {
                    var go = Instantiate(tilePrefab);
                    go.name = "Tile" + i;
                    go.transform.SetParent(mapContainer.transform);
                    go.transform.position = new Vector3(newX, newY, 0);
                    var sr = go.GetComponent<SpriteRenderer>();
                    go.AddComponent<BoxCollider2D>().size = new Vector2(10f, 10f);
                    //sr.sprite = sprites[15];
                    sr.enabled = false;
                }
                i++;
            }
        }
    }
    void ClearMapContainer()
    {

        var children = mapContainer.transform.GetComponentsInChildren<Transform>();
        for (var i = children.Length - 1; i > 0; i--)
        {
            Destroy(children[i].gameObject);
        }

    }

    void CenterMap(int index)
    {

        var camPos = Camera.main.transform.position;
        var width = map.columns;
        camPos.x = (index % width) * tileSize.x;
        camPos.y = -((index / width) * tileSize.y);
        Camera.main.transform.position = camPos;

    }
}
