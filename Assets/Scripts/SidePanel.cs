using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class SidePanel : MonoBehaviour
{
    public Button openButton;
    public Button closeButton;
    public RectTransform panel;
    
    public GameObject missionPrefab;
    
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
    
    public void AddMission(MissionData mission)
    {
        var content = panel.GetComponentInChildren<ScrollRect>().content;
        GameObject missionObj = Instantiate(missionPrefab, content);
    }
}
