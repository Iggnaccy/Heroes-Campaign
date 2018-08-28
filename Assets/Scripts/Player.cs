using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private float lastSalaryTime=0;

    public int Fame { get; set; }

    public double Gold { get; set; }
    public string Name { get; private set; }

    public List<Hero> Heroes { get; private set; }

    public Player(string name, int fame, int gold) {
        Name = name;
        Fame = fame;
        Gold = gold;
        Heroes = new List<Hero>();
    }

    private void PaySalary()
    {
        float delta = Time.deltaTime;
        lastSalaryTime += delta;
        if (lastSalaryTime > StaticValues.SalaryIntervalInSeconds) {
            lastSalaryTime = 0;
            for (int i = Heroes.Count - 1; i >= 0; i--) {
                Hero hero = Heroes[i];
                double goldChange = (StaticValues.SalaryIntervalInSeconds / (float)StaticValues.SalaryDescriptionIntervalInSeconds)
                    * hero.Salary;
                if (Gold - goldChange > 0)
                    Gold -= goldChange;
                else {
                    //if(!(have_payed_1000_drgn_coins_to_save_hero)){
                    hero.Fire();
                    Heroes.RemoveAt(i);
                    //}
                }
            }
        }
    }

    public void Update() {
        PaySalary();
    }

    public bool RecruitHero(Hero hero)
    {
        Gold -= hero.Cost;
        Heroes.Add(hero);
        return true;
    }

}
