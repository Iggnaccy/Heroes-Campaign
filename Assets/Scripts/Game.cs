using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour {

    private Player player;
    private System.Random rng;

    private List<Hero> availableHeroes;
    private List<Kingdom> locations;

	void Start () {
        rng = new System.Random();
        player = new Player("test", 0, 1000);
        availableHeroes = new List<Hero>();
        spawnHeroes(5);
        foreach (Hero hero in availableHeroes)
        {
            hero.Log();
            hero.Stats.Log();
            hero.LevelUp();
            hero.Log();
            hero.Stats.Log();
        }

        locations = new List<Kingdom>();
        GenerateExampleKingdoms(5);

        GameObject MissionHolder;
        MissionHolder = new GameObject();

        //Przykład jak dodawać nowe misje, potem się usunie
        Mission example = new Mission("name", "", 10.0, 31, -1, 0, 0, 0, 0, Mission.MissionTypes.Exploration);
        MissionHolder.AddComponent<OngoingMission>();
        MissionHolder.GetComponent<OngoingMission>().GameReference=this;
        MissionHolder.GetComponent<OngoingMission>().SelectedMission =example;
        MissionHolder.GetComponent<OngoingMission>().ParticipatingHeroes = availableHeroes;
    }
	
	void Update () {
		
	}

    public void spawnHeroes(int count) {
        for(int i = 0; i < count; i++)
            availableHeroes.Add(HeroGenerator.GenerateHero(rng));
    }

    public void GenerateExampleKingdoms(int count)
    {
        for(int i=0; i<count; i++)
        {
            locations.Add(new Kingdom("Kingdom" + i.ToString(), i, ""));
        }
    }





    public void changeChaosLevels(int BitMask, int amount)
    {
        for(int i=0; i<locations.Count; i++)
        {
            locations[i].Chaos = System.Math.Max(0, locations[i].Chaos + amount *(1&(BitMask>>i)));
        }
    }

    public void changeFame(int amount)
    {
        player.Fame += amount;
    }

    public void changeGold(int amount)
    {
        player.Gold += amount;
    }


}
