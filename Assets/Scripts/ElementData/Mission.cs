using System;
using UnityEngine;

[Serializable]
public class Mission
{
    public string name;
    [TextArea(3,3)]
    public string description;
    public int money;
    public MissionPanel missionPanel;
}
