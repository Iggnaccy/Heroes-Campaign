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
    public int Exp { get; set; }
    public int Level { get; set; }
    public string Name { get; private set; }
    public HeroProfession Profession { get; private set; }
    public int Salary { get; set; }

    public Hero(string name, HeroProfession profession, int exp, int level, int salary) {
        Name = name;
        Profession = profession;
        Exp = exp;
        Level = level;
        Salary = salary;
    }
}
