using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MissionSpawnner : MonoBehaviour
{
    [SerializeField] private int missionMax = 4; 
    private int missionMaxIndex = 0;
    [SerializeField] private MissionPoint missionPoint;
    [SerializeField] private List<Transform> missionPoints;
    [SerializeField] private List<int> pointMissions = new();
     private List<Transform> missionPointIndex;
    [SerializeField] private MissionData[] missionDataN0;
    [SerializeField] private MissionData[] missionDataN1;
    [SerializeField] private MissionData[] missionDataN2;

    [SerializeField] private float duration = 5f;
    private float cooldown = 0f;
    private void Update()
    {
        cooldown += Time.deltaTime / duration;
        //Debug.Log(missionMaxIndex + "  <  " + missionMax + " cooldown : " + cooldown);
        
        if (missionMaxIndex <= missionMax && cooldown >= 1f)
        {
            missionMaxIndex++;
            int rdPos = Random.Range(0,missionPoints.Count);
            int rdMis = Random.Range(0,missionDataN0.Length);
                                                    
            if(pointMissions.Contains(rdPos)) return;
                                                                                
            pointMissions.Add(rdPos);
                                                                                                                                                            
            MissionPoint me = Instantiate(missionPoint, missionPoints[rdPos].position, Quaternion.identity);
            me.SetMissionData(missionDataN0[rdMis]);
            cooldown = 0f;
        }
    }
}
