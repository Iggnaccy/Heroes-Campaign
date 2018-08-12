using System.Collections;
using System.Collections.Generic;

public class Hero {
    public enum HeroProfession {
        Archer,
        Druid,
        Intern,
        Knight,
        Mage,
        Rogue,
        Warrior
    };
    public int Cost { get; set; }
    public int Exp { get; set; }
    public int Level { get; set; }
    public string Name { get; private set; }
    public HeroProfession Profession { get; private set; }
    public int Salary { get; set; }

    //there should be another construcor for reading saved Heros from file/db
    public Hero(string name, HeroProfession profession, int cost, int salary)
    {
        Name = name;
        Profession = profession;
        Cost = cost;
        Exp = StaticValues.startingExp;
        Level = StaticValues.startingLevel;
        Salary = salary;
    }
}
