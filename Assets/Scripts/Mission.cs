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

    //public Game GameReference;
    public List<Hero> ParticipatingHeroes { get; set; }


    public string MissionName { get; private set; }
    public string MissionDescription { get; private set; }
    public double MissionTime { get; private set; }
    public int Kingdoms { get; private set; }
    public int ChaosReduction { get; private set; }
    public int GoldEarned { get; private set; }
    public int ExpEarned { get; private set; }
    public int FameEarned { get; private set; }
    public MissionTypes MissionType { get; private set; }
    

    public Mission(string missionName, string missionDescription, double missionTime, int kingdoms, int chaosReduction, int goldEarned, int expEarned, int fameEarned, MissionTypes missionType)
    {
        ParticipatingHeroes = new List<Hero>();


        MissionName = missionName;
        MissionDescription = missionDescription;

        MissionTime = missionTime;
        Kingdoms = kingdoms;

        ChaosReduction = chaosReduction;
        GoldEarned = goldEarned;
        ExpEarned = expEarned;
        FameEarned = fameEarned;

        MissionType = missionType;
    }


    public void BeginMission()
    {
        //System.Timers.Timer TimeUntilEnd=new System.Timers.Timer();
        //TimeUntilEnd.Interval = MissionTime * 1000;
        double DeltaTime=MissionTime;
    }

   /* public void Victory()
    {
        for (int i = 0; i < ParticipatingHeroes.Count; i++)
        {
            if (ParticipatingHeroes[i].Level < StaticValues.LevelCap)
            {
                ParticipatingHeroes[i].Exp += ExpEarned;
                while (ParticipatingHeroes[i].Exp > StaticValues.ExpNeededToNextLevel[ParticipatingHeroes[i].Level])
                {
                    ParticipatingHeroes[i].Exp -= StaticValues.ExpNeededToNextLevel[ParticipatingHeroes[i].Level];
                    ParticipatingHeroes[i].Level++;
                    if (ParticipatingHeroes[i].Level == StaticValues.LevelCap)
                    {
                        ParticipatingHeroes[i].Exp = 0;
                        break;
                    }
                }
            }
        }


        GameReference.changeChaosLevels(Kingdoms, -ChaosReduction);
        GameReference.changeFame(FameEarned);
        GameReference.changeGold(GoldEarned);


    }*/




    public void Defeat()
    {

    }



}
