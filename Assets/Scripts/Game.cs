using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour {

    private Player player;
    private System.Random rng;

    private List<Hero> availableHeroes;
    private List<Kingdom> locations;
    private List<Mission> activeMission;

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
        generateExampleKingdoms(5);

        activeMission = new List<Mission>();
        //Przykład jak dodawać nowe misje, potem się usunie
        Mission example = new Mission(this);
        example.SetHeroes(availableHeroes);
        activeMission.Add(example);

        
    }

	void Update () {
        for(int i=0; i<activeMission.Count; i++)
        {
            activeMission[i].RemainingTime -= Time.deltaTime;
            if(activeMission[i].RemainingTime<=0)
            {
                activeMission[i].Victory();
                activeMission.RemoveAt(i);
                i--;
            }
        }
		
	}

    public void spawnHeroes(int count) {
        for(int i = 0; i < count; i++)
            availableHeroes.Add(HeroGenerator.GenerateHero(rng));
    }

    public void generateExampleKingdoms(int count)
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

    public int getNumberOfKingdoms()
    {
        return locations.Count;
    }
}
