﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public SidePanel SidePanel;

    [SerializeField] private Tilemap roadTilemap;

    private bool _pathMode;
    private List<List<Vector3Int>> _path = new List<List<Vector3Int>>();
    
    public List<List<Vector3Int>> Path => _path;

    private List<Vector3Int> _startPoints = new List<Vector3Int>();

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

        _pathMode = true;
        
        _startPoints = new List<Vector3Int>
        {
            new (48, 52, 0),
            new (52, 52, 0),
            new (52, 56, 0),
            new (48, 56, 0)
        };
    }

    private void Update()
    {
        if (!_pathMode) return;
        roadTilemap.RefreshAllTiles();
        _path.ForEach(x =>
        {
            x.ForEach(y =>
            {
                roadTilemap.SetTileFlags(y, TileFlags.None);
                roadTilemap.SetColor(y, Color.green);
            });
        });

        var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
     
        var tpos = roadTilemap.WorldToCell(worldPoint);
        tpos.z = 0;
        print(tpos);

        // Try to get a tile from cell position
        var tile = roadTilemap.GetTile(tpos);
        
        var currentPoints = new List<Vector3Int>();

        if(tile)
        {
            print("In tile");
            int modX = tpos.x % 4;
            int modY = tpos.y % 4;
            if (modX == 0 && modY != 0)
            {
                var start = tpos.y - modY;
                for (int i = start; i < start + 5; i++)
                {
                    currentPoints.Add(new Vector3Int(tpos.x, i, 0));
                }
            } if (modY == 0 && modX != 0)
            {
                var start = tpos.x - modX;
                for (int i = start; i < start + 5; i++)
                {
                    currentPoints.Add(new Vector3Int(i, tpos.y, 0));
                }
            }
        }

        Color color = Color.red;
        if (_path.Count == 0 && currentPoints.Any(x => _startPoints.Contains(x)))
        {
            color = Color.green;
        }
        else if (_path.Count > 0
                 && currentPoints.Any(x => _path.Last().Contains(x))
                 && !currentPoints.Any(x => _path.SkipLast(1).Any(y => y.Contains(x))))
        {
            color = Color.green;
        }

        currentPoints.ForEach(x =>
        {
            roadTilemap.SetTileFlags(x, TileFlags.None);
            roadTilemap.SetColor(x, color);
        });

        if (Input.GetMouseButtonDown(0) && currentPoints.Count > 0)
        {
            if (color == Color.green )
            {
                _path.Add(currentPoints);
            }
        }

        _path.ForEach(x =>
        {
            x.ForEach(y =>
            {
                roadTilemap.SetTileFlags(y, TileFlags.None);
                roadTilemap.SetColor(y, Color.green);
            });
        });
    }
}