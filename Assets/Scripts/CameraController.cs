using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float zoomSpeed;
    public float minZoomDist;
    public float maxZoomDist;
    
    private Vector3 _lastMousePos;

    private Camera _cam;
    bool IsMouseOverGameWindow => 
        !(0 > Input.mousePosition.x
          || 0 > Input.mousePosition.y
          || Screen.width < Input.mousePosition.x
          || Screen.height < Input.mousePosition.y);


    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        Move();
    }

    void Zoom()
    {
        if (!IsMouseOverGameWindow) return;
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        _cam.orthographicSize += -scrollInput * zoomSpeed * Time.deltaTime;
    }
    
    void Move()
    {
        if (!Input.GetMouseButton(1)) return;
        
        Vector3 delta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        
        Vector3 dir = -delta;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
