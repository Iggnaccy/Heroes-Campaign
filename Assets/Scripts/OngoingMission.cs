using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngoingMission : MonoBehaviour
{

    public Mission SelectedMission { get; set; }
    public List<Hero> ParticipatingHeroes { get; set; }
    public Game GameReference { get; set; }

    public double RemainingTime { get; private set; }

    void Start()
    {
        RemainingTime = SelectedMission.MissionTime;
    }


	
	void Update () {
        RemainingTime -= Time.deltaTime;
        if(RemainingTime<=0)
        {
            Victory();
        }
	}

    private void Victory()
    {
        GameReference.changeChaosLevels(SelectedMission.Kingdoms, -SelectedMission.ChaosReduction);
        GameReference.changeFame(SelectedMission.FameEarned);
        GameReference.changeGold(SelectedMission.GoldEarned);

        for(int i=0; i<ParticipatingHeroes.Count; i++)
        {
            ParticipatingHeroes[i].GainExp(SelectedMission.ExpEarned);
        }
        //To Do zwracanie poiwadomienia o ukończeniu misji

        Debug.Log("Mission Acomplished");

        Destroy(this);
    }




}
