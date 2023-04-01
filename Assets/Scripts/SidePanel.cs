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
    public Button toggleButton;
    public RectTransform panel;
    
    public GameObject missionPrefab;

    private List<MissionElement> _elements = new ();
    
    private bool _isOpen;
    
    static int lastMissionId = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        toggleButton.onClick.AddListener(() =>
        {
            if (!_isOpen)
            {
                panel.gameObject.SetActive(true);
                panel.DOMoveX(250.0f, 0.3f, true).OnComplete(() => _isOpen = true);
                return;
            }
            panel.DOMoveX(-250.0f, 0.3f, true).OnComplete(() => _isOpen = false);
        });
    }
    
    public void AddMission(MissionData mission)
    {
        var content = panel.GetComponentInChildren<ScrollRect>().content;
        var missionElement = Instantiate(missionPrefab, content).GetComponent<MissionElement>();
        missionElement.Init(mission, new Vector3(-0.5f + lastMissionId*3, -4.25f + lastMissionId*1.5f, 0f));
        _elements.Add(missionElement);
        lastMissionId++;
    }
}
