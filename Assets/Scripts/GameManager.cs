using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager intance;
    public SidePanel SidePanel;

    [SerializeField] private Tilemap roadTilemap;

    private bool _pathMode;
    private List<List<Vector3Int>> _path = new List<List<Vector3Int>>();
    
    public List<List<Vector3Int>> Path => _path;

    private List<Vector3Int> _startPoints = new List<Vector3Int>();
    [SerializeField] private Button confirmPath;
    [SerializeField] private TruckMouvement truck;

    // Start is called before the first frame update

    private void Awake()
    {
        intance = this;
    }

    void Start()
    {
        confirmPath.onClick.AddListener(ConfirmPath);
        
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
    void ConfirmPath()
    {
        truck.patrolPoints = new List<Vector3>();
        
        
        for (int i = 0; i < _path.Count; i++)
        {
//            var _start = _path[i][0];
//            var _ending = _path[i][_path[i].Count];

//Debug.Log($"i+1 = {i+1} < {_path.Count} && {_path[i][0]}  == {_path[i+1][_path[i+1].Count]}");
Debug.Log($"i+1 = {i+1} < {_path.Count} ");
Debug.Log($"&& {_path[i][0]} ");
if((i + 1) < _path.Count) Debug.Log($"&& {_path[i+1][_path[i+1].Count-1]} ");

            if ((i + 1) < _path.Count && _path[i][0] == _path[i + 1][_path[i + 1].Count-1])
            {
                Debug.Log("retour");
                for (int j = _path[i].Count; j < 0; j--)
                {
                    truck.patrolPoints.Add(roadTilemap.CellToWorld(_path[i][j]));
                }
            }
            else
            {
                for (int j = 0; j < _path[i].Count; j++)
                {
                    truck.patrolPoints.Add(roadTilemap.CellToWorld(_path[i][j]));
                }
            }
        }
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

        // Try to get a tile from cell position
        var tile = roadTilemap.GetTile(tpos);
        var currentPoints = new List<Vector3Int>();

        if(tile)
        {
            int modX = tpos.x % 4;
            int modY = tpos.y % 4;
            
            if (modX == 0 && modY != 0)
            {
                var start = tpos.y - modY;
                for (int i = start; i < start + 5; i++)
                {
                    currentPoints.Add(new Vector3Int(tpos.x, i, 0));
                }
            } 
            
            if (modY == 0 && modX != 0)
            {
                var start = tpos.x - modX;
                for (int i = start; i < start + 5; i++)
                {
                    currentPoints.Add(new Vector3Int(i, tpos.y, 0));
                }
            }
        }

        Color color = Color.red;
        bool removeLast = false;
        if (_path.Count == 0 && currentPoints.Any(x => _startPoints.Contains(x)))
        {
            color = Color.green;
        }
        else if (_path.Count > 0)
        {
            if(currentPoints.Any(x => _path.Last().Contains(x))
               && !currentPoints.Any(x => _path.SkipLast(1).Any(y => y.Contains(x))))
            {
                color = Color.green;
            }
            if (currentPoints.All(x => _path.Last().Contains(x)))
            {
                removeLast = true;
            }
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

            if (removeLast)
            {
                _path.RemoveAt(_path.Count - 1);
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