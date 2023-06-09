using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
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
        missionElement.Init(mission, new Vector3(44 + lastMissionId*3, 8 + lastMissionId*1.5f, 0f), missionElement);
        _elements.Add(missionElement);
        lastMissionId++;
    }
    public void RemoveMission(MissionElement mission)
    {  
        _elements.Remove(mission);
        Destroy(mission.gameObject);
        lastMissionId--;
    }
}
