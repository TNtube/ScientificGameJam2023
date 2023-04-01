using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SidePanel SidePanel;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var missionData = ScriptableObject.CreateInstance<MissionData>();
            missionData.title = $"Mission {i}";
            missionData.description = $"Ceci est la mission numéro {i}, Pedro a besoin de son paquet en urgence !";
            missionData.reward = Random.Range(50, 950);
            SidePanel.AddMission(missionData);
        }
    }
}