using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    //Peut etre privé
  [SerializeField] private MissionData mission;
  private Canvas canvas;
  [SerializeField] private Button closeUI, acceptUI, declineUI;
  
  [SerializeField] private TextMeshProUGUI titleTxt, descriptionTxt, moneyTxt;
  public Canvas GetCanvas { set => canvas = value; get => canvas; }

  private void Start()
  {
      canvas = GetComponent<Canvas>();
      closeUI.onClick.AddListener(CloseUI);
      acceptUI.onClick.AddListener(CloseUI);
      declineUI.onClick.AddListener(CloseUI);
  }

  public void UpdateView(MissionData _newMission)
  {
      mission = _newMission;
      titleTxt.text = $"{mission.title}";
      descriptionTxt.text = $"{mission.description}";
      moneyTxt.text = $"{mission.reward}";
  }
  void CloseUI()
  {
      Debug.Log("Close ! ");
      //Action 
      canvas.enabled = false;
      
      //Animation
  }
  void AcceptUI()
  {
      Debug.Log("Accept ! ");
      
      //Action 
      
      //Animation
  }
  void DeclineUI()
  {
      Debug.Log("Decline ! ");
      
      //Action 
      
      //Animation
  }
}
