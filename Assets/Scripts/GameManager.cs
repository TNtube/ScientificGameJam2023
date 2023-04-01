using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public SidePanel SidePanel;

    [SerializeField] private Tilemap roadTilemap;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var missionData = ScriptableObject.CreateInstance<MissionData>();
            missionData.title = $"Mission {i}";
            missionData.description = $"Ceci est la mission numéro {i}, Pedro a besoin de son paquet en urgence !";
            missionData.reward = Random.Range(50, 950);
            SidePanel.AddMission(missionData);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         
            var tpos = roadTilemap.WorldToCell(worldPoint);
            tpos.z = 0;
            print(tpos);

            // Try to get a tile from cell position
            var tile = roadTilemap.GetTile(tpos);

            if(tile)
            {
                roadTilemap.RefreshAllTiles();
                print("In tile");
                Vector3Int intersectionA = tpos;
                Vector3Int intersectionB = tpos;
                int modX = tpos.x % 4;
                int modY = tpos.y % 4;
                if (modX == 0 && modY != 0)
                {
                    var start = tpos.y - modY;
                    for (int i = start; i < start + 5; i++)
                    {
                        roadTilemap.SetTileFlags(new Vector3Int(tpos.x, i, 0), TileFlags.None);
                        roadTilemap.SetColor(new Vector3Int(tpos.x, i, 0), Color.red);
                    }
                } if (modY == 0 && modX != 0)
                {
                    var start = tpos.x - modX;
                    for (int i = start; i < start + 5; i++)
                    {
                        roadTilemap.SetTileFlags(new Vector3Int(i, tpos.y, 0), TileFlags.None);
                        roadTilemap.SetColor(new Vector3Int(i, tpos.y, 0), Color.red);
                    }
                }
                Debug.Log($"Intersection B {intersectionB} | Intersection A {intersectionA} | ModX {modX} | ModY {modY}");
                // roadTilemap.SetTileFlags(intersectionA, TileFlags.None);
                // roadTilemap.SetColor(intersectionA, Color.red);
                // roadTilemap.SetTileFlags(intersectionB, TileFlags.None);
                // roadTilemap.SetColor(intersectionB, Color.red);
                
            }
        }
    }
}