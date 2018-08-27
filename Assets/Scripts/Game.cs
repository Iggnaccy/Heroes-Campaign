using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour {

    public Player Player { get; private set; }
    private System.Random rng;
    public GameObject PlayerStatsPanel;
    public GameObject EndingPanel;

    public List<Hero> AvailableHeroes { get; private set; }
    public List<Kingdom> Locations { get; private set; }

    public List<Mission> Missions { get; private set; }
    public List<Mission> ActiveMission { get; private set; }
    public List<Mission> CompletedMission { get; private set; }

    void StartGame()
    {
        EndingPanel.SetActive(false);
        rng = new System.Random();
        Player = new Player("test", 0, StaticValues.InitialGold);
        PlayerStatsPanel.GetComponent<PlayerStatsPanel>().SetPlayer(Player);
        Missions = new List<Mission>
        {
            new Mission(),
            new Mission(),
            new Mission(),
            new Mission(),
            new Mission()
        };
        ActiveMission = new List<Mission>();
        CompletedMission = new List<Mission>();
        Locations = new List<Kingdom>
        {
            new Kingdom("The Kingdom of Farmers",1,"This kingdom is ruled by the great farmer, Szamek.\nIf you're not a farmer, you wouldn't want to stay there for too long, as lord Szamek doesn't like people that don't farm."),
            new Kingdom("The Kingdom of Bankers",2,"2"),
            new Kingdom("The Kingdom of Smiths",3,"3"),
            new Kingdom("The Kingdom of Nokia",4,"4"),
            new Kingdom("The Kingdom of Magazynierzy",5,"5"),
            new Kingdom("The Kingdom of Interns",6,"6")
        };
        GetComponent<KingdomOverviewPanelScript>().SetKingdomNames(Locations);

        /*
        GetComponent<GuildManagementGUIDisplay>().DisplayHeroButtons(Player);
        GetComponent<MissionAssignmentGUI>().DisplayMissionPanel(this);
        GetComponent<HeroRecruitmentGUI>().DisplayHeroRecruitment(this);
        */

        AvailableHeroes = new List<Hero>();
        SpawnHeroes(5);
        foreach (Hero hero in AvailableHeroes)
        {
            hero.Log();
            hero.Stats.Log();
            hero.LevelUp();
            hero.Log();
            hero.Stats.Log();
        }
    }
    void Start()
    {
        StartGame();
    }
	
	void Update ()
    {
        for (int i = 0; i < ActiveMission.Count; i++)
        {
            ActiveMission[i].RemainingTime -= Time.deltaTime;
            if (ActiveMission[i].RemainingTime <= 0)
            {
                ActiveMission[i].Victory();
                CompletedMission.Add(ActiveMission[i]);
                ActiveMission.RemoveAt(i);
                i--;
            }
        }
        foreach (Kingdom kingdom in Locations)
            kingdom.CheckIfAlive();
        if (Kingdom.ChaosOverwhelming >= StaticValues.MaxChaosOverwhelming)
        {
            GameOver();
        }
        Player.Update();
    }

    public void ChangeDisplay(int i)
    {
        var gm = GetComponent<GuildManagementGUIDisplay>();
        var ma = GetComponent<MissionAssignmentGUI>();
        var hr = GetComponent<HeroRecruitmentGUI>();
        var ko = GetComponent<KingdomOverviewPanelScript>();
        switch(i)
        {
            case 0:
                gm.Clear();
                ma.Clear();
                hr.Clear();
                break;
            case 1:
                gm.DisplayHeroButtons(Player);
                ma.Clear();
                hr.Clear();
                break;
            case 2:
                ma.DisplayMissionPanel(this);
                gm.Clear();
                hr.Clear();
                break;
            case 3:
                hr.DisplayHeroRecruitment(this);
                gm.Clear();
                ma.Clear();
                break;
            case 4:
                ko.UpdatePanels(Locations);
                gm.Clear();
                hr.Clear();
                ma.Clear();
                break;
        }
    }

    public void SpawnHeroes(int count) {
        for(int i = 0; i < count; i++)
            AvailableHeroes.Add(HeroGenerator.GenerateHero(rng));
    }

  
    public void ChangeChaosLevels(int BitMask, int amount)
    {
        for(int i=0; i<Locations.Count; i++)
        {
            Locations[i].Chaos = System.Math.Max(0, Locations[i].Chaos + amount *(1&(BitMask>>i)));
        }
    }

    public void ChangeFame(int amount)
    {
        Player.Fame += amount;
    }

    public void ChangeGold(int amount)
    {
        Player.Gold += amount;
    }

   /* public int GetNumberOfKingdoms()
    {
        return Locations.Count;
    }*/

    public void GameOver()
    {
        foreach(Transform t in EndingPanel.transform.parent)
        {
            t.gameObject.SetActive(false);
        }
        EndingPanel.SetActive(true);
    }
}
