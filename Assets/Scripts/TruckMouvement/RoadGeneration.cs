using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoadGeneration : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector2Int roadSize;
    [SerializeField] private int BuildingSize = 5;
    [SerializeField] private Vector2Int offSet = new Vector2Int(0,0);
    private List<TileData> CellListForNeigtour =new();
    private TileData[,] tileDatas;
    [SerializeField]    private Tile roadTile;
    private int CellIndexTotal = 0;
    private int CellIndexBuilding = 0;
    private void Start()
    {
        tileDatas = new TileData[roadSize.x, roadSize.y];
        CellIndexTotal = roadSize.x * roadSize.y;

        Generation();
    }

    void Generation()
    {
        for (int x = 0; x < tileDatas.GetLongLength(0); x++)
        {
            for (int y = 0; y < tileDatas.GetLongLength(1); y++)
            {
                tileDatas[x, y] = new TileData();
                tileDatas[x, y].coord = new Vector2Int(x, y);
                tileDatas[x, y].isRoad = false;
                CellListForNeigtour.Add(tileDatas[x, y]);

               // if(x == 0 || y == 0 ||x == 5 || y == 5||x == 10 || y == 10 ||x == 15 || y == 15)
               //if(CellIndexBuilding == 0 ||  CellIndexBuilding == BuildingSize)
               // Refaire (test temporaire)
               if(x%BuildingSize == 0 || y%BuildingSize == 0)
               {
                   tilemap.SetTile(new Vector3Int(x + offSet.x, y+ offSet.y, 0), roadTile);
                }
            }
        }
    }
    
    
    
    
    public void SetTileDataEmpty(int _coordX, int _coordY, bool _isEmpty)
    {
        tileDatas[_coordX, _coordY].isRoad = _isEmpty;
    }
    
    public TileData GetTileData(Vector2Int _coord)
    {
        Debug.Log($"TILE : x : {_coord.x + offSet.x} / y : {_coord.y+ offSet.y}");
        return  tileDatas[_coord.x + offSet.x, _coord.y+ offSet.y];
    }
    public bool CheckOutside(int _coordX, int _coordY)
    {
        if (_coordX >= 0 && _coordX < roadSize.x && _coordY >= 0 && _coordY < roadSize.y)
            return true;
        
        return false;
        
    }
    public List<TileData> GetNeighbours(TileData cell) 
    {
        List<TileData> neighbours = new List<TileData>();

        for (int x = -1; x <= 1; x++) 
        {
            for (int y = -1; y <= 1; y++) 
            {
                if (x == y)
                    continue;
                 
                int checkX = cell.coord.x + x;
                int checkY = cell.coord.y + y;

                if(cell.coord.x + x >= 0 &&  cell.coord.x+ x < CellIndexTotal && 
                   cell.coord.y+ y >= 0 && cell.coord.y+ y < CellIndexTotal && 
                   CheckOutside(cell.coord.x + x,cell.coord.y+ y )  &&
                   CellListForNeigtour.Contains(tileDatas[cell.coord.x + x, cell.coord.y + y]))
                {
                    neighbours.Add(tileDatas[checkX,checkY]);
                }
            }
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {
        /*
        tileDatas = new TileData[roadSize.x, roadSize.y];
        
        Gizmos.color = Color.blue;
        for (int x = 0; x < tileDatas.GetLongLength(0); x++)
        {
            for (int y = 0; y < tileDatas.GetLongLength(1); y++)
            {
                if (x % BuildingSize == 0 || y % BuildingSize == 0)
                {
                    Gizmos.DrawWireCube(new Vector3Int(x + offSet.x, y + offSet.y, 0), new Vector3(1f, 1f, 1f));
                }
            }
        }
        */
    }
}

public class TileData
{
    public Vector2Int coord = new();
    public bool isRoad = false;
}