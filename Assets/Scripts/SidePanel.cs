using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SidePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button openButton;
    public Button closeButton;
    public RectTransform panel;
    
    private CameraController _cam;
    
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main.GetComponent<CameraController>();
        openButton.onClick.AddListener(() =>
        {
            panel.gameObject.SetActive(true);
            panel.DOMoveX(250.0f, 0.3f, true);
        });
        
        closeButton.onClick.AddListener(() =>
        {
            panel.DOMoveX(-250.0f, 0.3f, true).OnComplete(() =>
            {
                panel.gameObject.SetActive(false);
            });
        });
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _cam.ShouldZoom = false;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        _cam.ShouldZoom = true;
    }
}
