using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour {

    private Player player;
    private System.Random rng;

    public List<Hero> AvailableHeroes { get; private set; }
    public List<Kingdom> Locations { get; private set; }
    public List<Mission> Missions { get; private set; }



    void Start () {
        rng = new System.Random();
        player = new Player("test", 0, 1000);
        AvailableHeroes = new List<Hero>();

        SpawnHeroes(5);
        foreach (Hero hero in AvailableHeroes)
        {
            hero.Log();
            hero.Stats.Log();
            player.RecruitHero(hero);
        }
        GetComponent<GuildManagementGUIDisplay>().DisplayHeroButtons(player);
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
        player.Fame += amount;
    }

    public void ChangeGold(int amount)
    {
        player.Gold += amount;
    }


}
