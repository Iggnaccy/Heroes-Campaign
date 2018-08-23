using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour {

    public Player Player;
    private System.Random rng;

    private List<Hero> AvailableHeroes;
    private List<Kingdom> Locations;
    private List<Mission> ActiveMission;
    private List<Mission> CompletedMission;

    void Start () {
        rng = new System.Random();
        Player = new Player("test", 0, 1000);
        AvailableHeroes = new List<Hero>();
        spawnHeroes(5);
        foreach (Hero hero in AvailableHeroes)
        {
            hero.Log();
            hero.Stats.Log();
            hero.LevelUp();
            hero.Log();
            hero.Stats.Log();
        }

        Locations = new List<Kingdom>();

        ActiveMission = new List<Mission>();
        CompletedMission = new List<Mission>();

        //Przykład jak dodawać nowe misje, potem się usunie


        /*Mission example = new Mission();
        example.SetHeroes(availableHeroes);
        activeMission.Add(example);*/

        
    }

	void Update () {
        for(int i=0; i<ActiveMission.Count; i++)
        {
            ActiveMission[i].RemainingTime -= Time.deltaTime;
            if(ActiveMission[i].RemainingTime<=0)
            {
                ActiveMission[i].Victory();
                CompletedMission.Add(ActiveMission[i]);
                ActiveMission.RemoveAt(i);
                i--;
            }
        }
		
	}

    public void spawnHeroes(int count) {
        for(int i = 0; i < count; i++)
            AvailableHeroes.Add(HeroGenerator.GenerateHero(rng));
    }






    public void changeChaosLevels(int BitMask, int amount)
    {
        for(int i=0; i<Locations.Count; i++)
        {
            Locations[i].Chaos = System.Math.Max(0, Locations[i].Chaos + amount *(1&(BitMask>>i)));
        }
    }

    public void changeFame(int amount)
    {
        Player.Fame += amount;
    }

    public void changeGold(int amount)
    {
        Player.Gold += amount;
    }

    public int getNumberOfKingdoms()
    {
        return Locations.Count;
    }
}
