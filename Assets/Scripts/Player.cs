using System.Collections;
using System.Collections.Generic;

public class Player {

    public int Fame { get; set; }
    public int Gold { get; set; }
    public string Name { get; private set; }

    public List<Hero> heroes { get; private set; }

    Player(string name, int fame, int gold) {
        Name = name;
        Fame = fame;
        Gold = gold;
        heroes = new List<Hero>();
    }

    public bool RecruitHero(Hero hero) {
        if (hero.Cost > Gold)
            return false;
        Gold -= hero.Cost;
        heroes.Add(hero);
        return true;
    }

    public bool PaySalary() {
        int totalCost = 0;
        foreach (Hero hero in heroes) 
            totalCost += hero.Salary;
        if (totalCost > Gold)
            return false;
        Gold -= totalCost;
        return true;
    }
}
