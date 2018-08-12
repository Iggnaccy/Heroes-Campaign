using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public enum HeroRace {
        Djin,
        Dwarf,
        Elf,
        Gnome,
        Goblin,
        Human,
        Orc,
        Skeletor,
        Troll,
        Vampire
    };
    public enum HeroSex {
        //Ah_64_Apache,
        Female,
        Male
    };

    public int Cost { get; set; }
    public int Exp { get; set; }
    public int Level { get; set; }
    public string Name { get; private set; }
    public HeroProfession Profession { get; private set; }
    public HeroRace Race { get; private set; }
    public HeroSex Sex { get; private set; }
    public int Salary { get; set; }

    //there should be another construcor for reading saved Heros from file/db
    public Hero(string name, HeroProfession profession, HeroRace race, HeroSex sex, 
        int cost, int salary)
    {
        Name = name;
        Profession = profession;
        Race = race;
        Sex = sex;
        Cost = cost;
        Exp = StaticValues.startingExp;
        Level = StaticValues.startingLevel;
        Salary = salary;
    }

    public void Log()
    {
        Debug.Log(string.Format("Name: {0}, Profession: {1}, Race: {2}, Sex: {3}, Cost: {4}, " +
            "Salary: {5}, Exp: {6}, Level: {7}", Name, Profession, Race, Sex, Cost, Salary, Exp, Level));
    }
}
