using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour {

    private Player player;
    private System.Random rng;

    private List<Hero> availableHeroes;
    private List<Kingdom> locations;
    private List<Mission> missions;

    

	void Start () {
        rng = new System.Random();
        player = new Player("test", 0, 1000);
        availableHeroes = new List<Hero>();

        SpawnHeroes(5);
        foreach (Hero hero in availableHeroes)
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
            availableHeroes.Add(HeroGenerator.GenerateHero(rng));
    }

    public void ChangeChaosLevels(int BitMask, int amount)
    {
        for(int i=0; i<32; i++)
        {
            locations[i].Chaos = System.Math.Max(0, locations[i].Chaos + amount *(1&(BitMask>>i)));
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
