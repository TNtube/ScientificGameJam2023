using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class MissionPoint : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Base a revoir
    public SidePanel SidePanel;
   public MissionData mission;
   public MissionData missionDATA;
   
    private Vector3 origineScale;
    private MissionPanel missionPanel;
    //public Canvas GetCanvas { set => canvas = value; get => canvas; }
    private void Start()
    {
        missionPanel = FindObjectOfType<MissionPanel>();
        SidePanel = FindObjectOfType<SidePanel>();
    }
    public void SetMissionData(MissionData _mission)
    {
        mission = _mission;
        
        //SidePanel.AddMission(_mission);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        missionPanel.GetCanvas.enabled = true;
       missionPanel.UpdateView(mission); 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        origineScale = transform.localScale;
        transform.localScale = new Vector3(origineScale.x + 0.2f, origineScale.y + 0.2f, 1);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = origineScale;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.transform.name);
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);   
        }   
    }
}
