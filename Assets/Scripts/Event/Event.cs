using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Event : MonoBehaviour
{

    public double probability { get; private set; } // <0,1>
    public Player player { get; set; }
    System.Random random;


    public enum EventTypes
    {
        Special_Mission,
        New_Hero,
        Gold_Was_Found,
        Increase_Of_Upkeep_Cost
    }

   // public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);

    public event EventHandler<SpecialMissionArgs> SpecialMission;
    public event EventHandler<NewHeroArgs> NewHero;
    public event EventHandler<GoldWasFoundArgs> GoldWasFound;
    public event EventHandler<IncreaseOfUpkeepCostArgs> IncreaseOfUpkeepCost;
                              


    // Use this for initialization
    void Start()
    {
        probability = 0;
        random = new System.Random();
       
    }

    // Update is called once per frame
    void Update()
    {
        probability += 0.01 * TimeManager.DeltaTime;
        bool t = false;
        if (probability >= random.NextDouble() && SpecialMission != null)
        {
            t = true;
            SpecialMission(this, new SpecialMissionArgs(player));
        }
        if (probability >= random.NextDouble() && NewHero != null)
        {
            t = true;
            NewHero(this, new NewHeroArgs(player));
        }
        if (probability >= random.NextDouble() && GoldWasFound != null)
        {
            t = true;
            GoldWasFound(this, new GoldWasFoundArgs(player));
        }
        if (probability >= random.NextDouble() && IncreaseOfUpkeepCost != null)
        {
            t = true;
            IncreaseOfUpkeepCost(this, new IncreaseOfUpkeepCostArgs(player));
        }

        if (t == true)
            probability = 0;
    }

}