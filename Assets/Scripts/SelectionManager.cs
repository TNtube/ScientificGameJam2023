using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private InputReader _input;
   [SerializeField] private Camera camera;
    private void Start()
    {
        _input=GetComponent<InputReader>();
    }

    private void Update()
    {  
        Vector2 positionMouse = camera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(positionMouse, Vector2.zero, 10f);
        
        if (hit.transform == null) return;

        Debug.Log(hit.transform.name);

        if(_input.Click) 
            Debug.Log("Click ! ");
        
        _input.Click = false;
    }
}
