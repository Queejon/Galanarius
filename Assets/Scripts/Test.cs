using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    private GridPartial<Tile> tiles;
    public GridPartial<Tile> Tiles
    {
        get
        {
            return tiles;
        }
    }

    private Tile[,] tilesInternal;

    // Start is called before the first frame update
    void Start()
    {
        tilesInternal = new Tile[20, 20];
        tiles = new GridPartial<Tile>(20, 20, 10f, Vector3.zero, (GridPartial<Tile> grid, int x, int y) => 
        {
            GameObject obj = Instantiate(tile);
            Tile t = obj.GetComponent<Tile>();
            t.xCoord = x;
            t.yCoord = y;

            tilesInternal[x, y] = t;
            return t;
        });
    }
}
