using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour {

    public Player Player { get; private set; }
    private System.Random rng;

    public List<Hero> AvailableHeroes { get; private set; }
    public List<Kingdom> Locations { get; private set; }
    public List<Mission> Missions { get; private set; }



    void Start () {
        rng = new System.Random();
        Player = new Player("test", 0, 1000);
        AvailableHeroes = new List<Hero>();
        Missions = new List<Mission>
        {
            new Mission("test1", "test1", 150.0, 1, 4, 24, 5, -6, Mission.MissionTypes.Escort),
            new Mission("test2", "test2", 130.0, 16, 2, 92, 15, 15, Mission.MissionTypes.Exploration),
            new Mission("test3", "test3", 152.0, 8, 48, 21, 35, 7, Mission.MissionTypes.Defence),
            new Mission("test4", "test4", 250.0, 4, 41, 241, 54, 6, Mission.MissionTypes.Extermination),
            new Mission("test5", "test5", 190.0, 2, 9, 22, 9, 26, Mission.MissionTypes.Escort)
        };

        SpawnHeroes(5);
        foreach (Hero hero in AvailableHeroes)
        {
            hero.Log();
            hero.Stats.Log();
            Player.RecruitHero(hero);
        }
        GetComponent<GuildManagementGUIDisplay>().DisplayHeroButtons(Player);
        GetComponent<MissionAssignmentGUI>().DisplayMissionPanel(this);
    }
	
	void Update () {
		
	}

    public void SpawnHeroes(int count) {
        for(int i = 0; i < count; i++)
            AvailableHeroes.Add(HeroGenerator.GenerateHero(rng));
    }

    public void ChangeChaosLevels(int BitMask, int amount)
    {
        for(int i=0; i<32; i++)
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


}
