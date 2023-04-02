using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionElement : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI reward;
     public MissionElement missionPoint;
    
    private CameraController _cameraController;
    
    void Start()
    {
        _cameraController = Camera.main.GetComponent<CameraController>();
    }

    public void Init(MissionData data, Vector3 position, MissionElement _missionElement)
    {
        transform.position = position;
        icon.sprite = data.icon;
        title.text = data.title;
        description.text = data.description;
        reward.text = $"{data.reward}$";
        missionPoint = _missionElement;
        
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Debug.Log("Mission accepted !");
            _cameraController.FocusAndZoom(position, 2.5f);
        });
    }
}
