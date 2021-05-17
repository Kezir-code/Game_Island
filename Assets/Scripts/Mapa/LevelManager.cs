using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tiles;

    [SerializeField]
    private Transform map;

    public GameObject currentPos;

    public Dictionary<Point, TileScript> Tiles { get; set; }

    public float TileSize
    {
        get { return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    void Start()
    {
        CreateLevel();
    }

    void Update()
    {

    }

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData = ReadLevelText();

        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        //Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapXSize; x++)
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }

    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex;
        int value;
        if(int.TryParse(tileType, out value) == true)
        {
            
            tileIndex = value;
        }
        else
        {
            tileIndex = 0;
        }

        //Debug.Log(tileIndex);
        if (tileIndex != 0)
        {
            // 9 - starting point
            if(tileIndex == 9)
            {
                TileScript newTile = Instantiate(tiles[1]).GetComponent<TileScript>();
                newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y) + 12, 10), map);
                newTile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
                // Distance
                //GameObject currentPos_ = Instantiate(currentPos);
                //currentPos_.transform.SetParent(map);
                //currentPos_.transform.position = newTile.transform.position;
            }
            else
            {
                TileScript newTile = Instantiate(tiles[tileIndex]).GetComponent<TileScript>();
                newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y) + 12, 10), map);
            }
            
        }

    }

    private string[] ReadLevelText()
    {
        TextAsset binddata = Resources.Load("Level1") as TextAsset;
        string data = binddata.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}
