using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public enum MissionTypes
    {
        Exploration,
        Escort,
        Extermination,
        Defence
    }




    public Game GameReference;
    public List<Hero> ParticipatingHeroes { get; set; }


    public string MissionName { get; private set; }
    public string MissionDescription { get; private set; }

    public double MissionTime { get; private set; }
    public double RemainingTime;

    public int Kingdoms { get; private set; }
    public int ChaosReduction { get; private set; }
    public int GoldEarned { get; private set; }
    public int ExpEarned { get; private set; }
    public int FameEarned { get; private set; }
    public int MissionDificulty { get; private set; }

    public MissionTypes MissionType { get; private set; }



    public Mission(string missionName, string missionDescription, double missionTime, int kingdoms, int chaosReduction, int goldEarned, int expEarned, int fameEarned, int missionDificulty, MissionTypes missionType)
    {
        GameReference = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        ParticipatingHeroes = new List<Hero>();

        MissionName = missionName;
        MissionDescription = missionDescription;

        MissionTime = missionTime;
        RemainingTime = MissionTime;
        Kingdoms = kingdoms;

        ChaosReduction = chaosReduction;
        GoldEarned = goldEarned;
        ExpEarned = expEarned;
        FameEarned = fameEarned;
        MissionDificulty = missionDificulty;

        MissionType = missionType;
    }


    public void SetHeroes(List<Hero> participatingHeroes)
    {
        foreach(Hero h in participatingHeroes)
        {
            ParticipatingHeroes.Add(h);
        }
    }

    public void Victory()
    {
        GameReference.ChangeChaosLevels(Kingdoms, -ChaosReduction);
        GameReference.ChangeFame(FameEarned);
        GameReference.ChangeGold(GoldEarned);

        for (int i = 0; i < ParticipatingHeroes.Count; i++)
        {
            Debug.Log("Awarding next hero");
            ParticipatingHeroes[i].Log();
            ParticipatingHeroes[i].GainExp(ExpEarned);
            ParticipatingHeroes[i].AssignedMission = null;
        }
        //To Do zwracanie poiwadomienia o ukończeniu misji

        Debug.Log("Mission Acomplished");
        GameObject.FindGameObjectWithTag("PopupHolder").GetComponent<PopupHolder>().MakeSimplePopup("Mission " + MissionName + " Acomplished", "Worrisome");
    }

    public Mission()
    {
        GameReference  = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        MissionName = MissionStrings.GetMissionName();
        MissionDescription = MissionStrings.GetMissionDescription();
        MissionDificulty = Random.Range(1, 10);

        MissionTime = Random.Range(5, 10) * 0.5;
        RemainingTime = MissionTime;

        int tmp = StaticValues.NumberOfKIngdoms;
        Kingdoms = Random.Range(1, (1 << tmp) - 1);
        ParticipatingHeroes = new List<Hero>();

        ChaosReduction = MissionDificulty;
        GoldEarned = 10 * Random.Range(1 << (MissionDificulty - 1), 1 << (MissionDificulty));
        ExpEarned = 10 * Random.Range(1 << (MissionDificulty - 1), 1 << (MissionDificulty));
        FameEarned = 10 * MissionDificulty;

        MissionType = Utils.generateRandomEnum<MissionTypes>(new System.Random());

        Debug.Log(string.Format("MissionTime {0}, Kingdoms {1}, ChaosReduction {2}, GoldEarned {3}, ExpEarned {4}, FameEarned {5}, MissionDificulty {6} MissionType {7}",
            MissionTime, Kingdoms, ChaosReduction, GoldEarned, ExpEarned, FameEarned, MissionDificulty, MissionType));

    }




}
