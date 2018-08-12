using System.Collections;
using System.Collections.Generic;

public class Player {

    public int Fame { get; set; }
    public int Gold { get; set; }
    public string Name { get; private set; }

    public List<Hero> Heroes { get; private set; }

    Player(string name, int fame, int gold) {
        Name = name;
        Fame = fame;
        Gold = gold;
        Heroes = new List<Hero>();
    }

    public bool PaySalary()
    {
        int totalCost = 0;
        foreach (Hero hero in Heroes)
            totalCost += hero.Salary;
        if (totalCost > Gold)
            return false;
        Gold -= totalCost;
        return true;
    }

    public bool RecruitHero(Hero hero)
    {
        if (hero.Cost > Gold)
            return false;
        Gold -= hero.Cost;
        Heroes.Add(hero);
        return true;
    }

}
