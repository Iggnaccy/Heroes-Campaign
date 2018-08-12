using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission: MonoBehaviour
{
    public enum MissionTypes
    {
        Exploration,
        Escort,
        Extermination,
        Defence
    }





    public Kingdom AreaOfMission { get; private set; }
    public Hero ParticipatingHero { get; private set; }
    public Player PiggyBank { get; private set; }



    public int ChaosReduction { get; private set; }
    public int GoldEarned { get; private set; }
    public int ExpEarned { get; private set; }
    public int FameEarned { get; private set; }
    public MissionTypes MissionType { get; private set; }



    public Mission(ref Kingdom areaOfMission, ref Hero participatingHero, ref Player piggyBank, int chaosReduction, int goldEarned, int expEarned, int fameEarned, MissionTypes missionType)
    {
        AreaOfMission = areaOfMission;
        ParticipatingHero = participatingHero;
        PiggyBank = piggyBank;

        ChaosReduction = chaosReduction;
        GoldEarned = goldEarned;
        ExpEarned = expEarned;
        FameEarned = fameEarned;
        MissionType = missionType;
    }


    public virtual void BeginMission()
    {


    }

    public virtual void Victory()
    {
        if (ParticipatingHero.Level < StaticValues.LevelCap)
        {
            ParticipatingHero.Exp += ExpEarned;
            while (ParticipatingHero.Exp > StaticValues.ExpNeededToNextLevel[ParticipatingHero.Level])
            {
                ParticipatingHero.Exp -= StaticValues.ExpNeededToNextLevel[ParticipatingHero.Level];
                ParticipatingHero.Level++;
                if (ParticipatingHero.Level == StaticValues.LevelCap)
                {
                    ParticipatingHero.Exp = 0;
                    break;
                }
            }
        }


        PiggyBank.Fame += FameEarned;
        PiggyBank.Gold += GoldEarned;


        AreaOfMission.Chaos = System.Math.Max(0, AreaOfMission.Chaos - ChaosReduction);
    }




    public virtual void Defeat()
    {

    }



}
