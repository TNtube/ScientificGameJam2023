using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TruckMouvement : MonoBehaviour
{
    [SerializeField] private List<Vector3> patrolPoints = new();
    //[SerializeField] private ;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] InputReader inputReader;

    [SerializeField] private float duration = 1f;
    //[SerializeField] private AnimationCurve curve;
    [SerializeField] private float cooldown = 0f;
     private int tileIndex = 0 ;
     private RoadGeneration grid ;

     private void Start()
     {
         grid = FindObjectOfType<RoadGeneration>();
     }

     private void Update()
    {
        Mouvement();
             
         if(!inputReader.Click) return;
         
        Vector3 _coordTileInGrid = tilemap.WorldToCell(inputReader.MousePositionInWorld);
         
        Vector3 _coordInGrid = inputReader.MousePositionInWorld;
        
        Debug.Log($"_coordTileInGrid : {_coordTileInGrid}" );
        Debug.Log($"_coordTileInGrid : {_coordTileInGrid}" );
        TileData td = grid.GetTileData(new Vector2Int((int)_coordTileInGrid.x , (int)_coordTileInGrid.y));
        Debug.Log($"td : {td.coord}" );
        
       // Vector2 pos = Camera.main.ViewportToWorldPoint(Input.mousePosition);
        //_coordInGrid.x += 0.5f;
        //_coordInGrid.y += 0.25f;
        Debug.Log("COORD : " + _coordInGrid);
         patrolPoints.Add(_coordInGrid);
         
         inputReader.Click = false;
     }

     void Mouvement()
     {
         if(tileIndex >= patrolPoints.Count-1) return;
         
         cooldown += Time.deltaTime / duration;

         Vector3 dir = Vector3.Lerp(patrolPoints[tileIndex], patrolPoints[tileIndex + 1], cooldown);
         dir.z = 1f;
         transform.position = dir;
         Debug.Log($"patrolPoints[tileIndex] : {patrolPoints[tileIndex]}");
         Debug.Log($"patrolPoints[tileIndex + 1] : {patrolPoints[tileIndex + 1]}");
         if (cooldown >= 1f)
         {
             cooldown = 0f;
             tileIndex++;
         }
     }
}
