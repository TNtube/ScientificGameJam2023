using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MissionData", menuName = "Mission", order = 0)]
public class MissionData : ScriptableObject
{
    public string title;  
    public string description;
    public int reward;
    public int time;
    public int difficulty;
    public Sprite icon;
}