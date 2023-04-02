using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TruckMouvement : MonoBehaviour, IPointerDownHandler
{
     public List<Vector3> patrolPoints = new();
    //[SerializeField] private List<Vector3> patrolPoints = new();
   // patrolPointsNew // public List<Vector3> patrolPointsNew = new();
    
    [SerializeField] private Tilemap tilemap;
    [SerializeField] InputReader inputReader;
    
    [Header("Mouvement")]
    [SerializeField] Sprite[] spriteDirections;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float cooldown = 0f;
      SelectionManager selectionManager ;
     public bool isSelected = false;
    
    [Header("ANimation")]
     private Vector3 origineScale;
    [SerializeField] private float durationAnim = 1f;
    [SerializeField] private float cooldownAnim = 0f;
    [SerializeField] private AnimationCurve curve;
    
     private int tileIndex = 0 ;
     private RoadGeneration grid;
     
     private int looped = 0;

     private void Start()
     {
         selectionManager = FindObjectOfType<SelectionManager>();
         origineScale = transform.position;
         grid = FindObjectOfType<RoadGeneration>();
         
         //patrolPoints.Add(transform.position);
     }

     private void Update()
    {
        Mouvement();
        TruckAnimation();
             
         if(!inputReader.Click || !isSelected || selectionManager.truck != gameObject) return;
         inputReader.Click = false;
         if(EventSystem.current.IsPointerOverGameObject()) return;
         
        Vector3 _coordTileInGrid = tilemap.WorldToCell(inputReader.MousePositionInWorld);
         
        Vector3 _coordInGrid = inputReader.MousePositionInWorld;
        
       // Debug.Log($"_coordTileInGrid : {_coordTileInGrid}" );

//        TileData td = grid.GetTileData(new Vector2Int((int)_coordTileInGrid.x , (int)_coordTileInGrid.y));
  //      Debug.Log($"td : {td.coord}" );
        
       // Vector2 pos = Camera.main.ViewportToWorldPoint(Input.mousePosition);
        //_coordInGrid.x += 0.5f;
        //_coordInGrid.y += 0.25f;
      //  Debug.Log("COORD : " + _coordInGrid);
         patrolPoints.Add(_coordInGrid);
    }

     void Mouvement()
     {
         if (patrolPoints.Count == 0) return;
         if (looped == 2) return;
         if(tileIndex >= patrolPoints.Count - 1 && looped < 2)
         {
             if (GameManager.StartPoints.Any(x => patrolPoints.Last() == x))
             {
                 looped = 2;
                 return;
             }
             patrolPoints.Reverse(0, patrolPoints.Count);
             tileIndex = 0;
             looped++;
             return;
         }
         
         cooldown += Time.deltaTime / duration;

         Vector3 dir = Vector3.Lerp(patrolPoints[tileIndex], patrolPoints[tileIndex + 1], cooldown);
         dir.z = 1f;
         transform.position = dir;

         SetDirection(patrolPoints[tileIndex], patrolPoints[tileIndex + 1]);
       
         if (cooldown >= 1f)
         {
             cooldown = 0f;
             tileIndex++;
         }
     }
     void SetDirection(Vector3 _origine, Vector3 _ending)
     {
         DirectionSprite newDirection = DirectionSprite.right;

         Vector3 dir = _ending - _origine;

         if (dir.x > 0 && dir.y > 0) spriteRenderer.sprite = spriteDirections[2]; // right
         else if (dir.x < 0 && dir.y < 0)spriteRenderer.sprite = spriteDirections[3]; // left
         else if (dir.x > 0  && dir.y < 0) spriteRenderer.sprite = spriteDirections[1]; //up
         else if (dir.x < 0 && dir.y > 0) spriteRenderer.sprite = spriteDirections[0]; //down
     }

     void TruckAnimation()
     {
         cooldownAnim += Time.deltaTime / durationAnim;
         
         //transform.localScale += new Vector3( 1 + curve.Evaluate(cooldownAnim),1 -curve.Evaluate(cooldownAnim),1 );

         if (cooldownAnim >= 1f)
         {
             cooldownAnim = 0f;
         }
     }

     public void OnPointerDown(PointerEventData eventData)
     {
         Selected();
     }

     public void Selected()
     {
         Debug.Log("selectionné ! ");
         selectionManager.updateTruck();
         selectionManager.SetTruck(transform.gameObject);
         isSelected = true;
     }
}

public enum DirectionSprite
{
    right =0,    
    left =1,    
    up =2,    
    down =3,    
}
