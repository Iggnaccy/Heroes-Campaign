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

    public class HeroStats {
        public int agility { get; set; }
        public int health { get; set; }
        public int intelligence { get; set; }
        public int mana { get; set; }
        public int strength { get; set; }

        //TODO: change this to some generics kung-fu
        public static int paramsCount() {
            return 5;
        }

        public HeroStats() { }

        public HeroStats(int initialValue) {
            agility = initialValue;
            health = initialValue;
            intelligence = initialValue;
            mana = initialValue;
            strength = initialValue;
        }

        public HeroStats(int _agility, int _health, int _intelligence, int _mana, int _strength) {
            agility = _agility;
            health = _health;
            intelligence = _intelligence;
            mana = _mana;
            strength = _strength;
        }

        public static HeroStats operator +(HeroStats left, HeroStats right)
        {
            left.agility += right.agility;
            left.health += right.health;
            left.intelligence += right.intelligence;
            left.mana += right.mana;
            left.strength += right.strength;
            return left;
        }

        public void ClampToZero() {
            agility = Mathf.Max(agility, 0);
            health = Mathf.Max(health, 0);
            intelligence = Mathf.Max(intelligence, 0);
            mana = Mathf.Max(mana, 0);
            strength = Mathf.Max(strength, 0);
        }

        public void Log() {
            Debug.Log(string.Format("Agility: {0}, Health: {1}, Intelligence: {2}, Mana: {3}, Strength: {4}",
                agility, health, intelligence, mana, strength));
        }
    }

    public int Age { get; set; }
    public int Cost { get; set; }
    public int Exp { get; set; }
    public int Level { get; set; }
    public string Name { get; private set; }
    public HeroProfession Profession { get; private set; }
    public HeroRace Race { get; private set; }
    public HeroSex Sex { get; private set; }
    public HeroStats Stats {get; set;}
    public int Salary { get; set; }

    //there should be another construcor for reading saved Heros from file/db
    public Hero(string name, HeroProfession profession, HeroRace race, HeroSex sex, 
        HeroStats stats, int level, int cost, int salary, int age)
    {
        Name = name;
        Profession = profession;
        Race = race;
        Sex = sex;
        Stats = stats;
        Level = level;
        Cost = cost;
        Exp = StaticValues.startingExp;
        Salary = salary;
        Age = age;
    }

    public void Log()
    {
        Debug.Log(string.Format("Name: {0}, Profession: {1}, Race: {2}, Sex: {3}, Cost: {4}, " +
            "Salary: {5}, Exp: {6}, Level: {7}", Name, Profession, Race, Sex, Cost, Salary, Exp, Level));
    }

    public void GainExp(int amount)
    {
        if (Level < StaticValues.LevelCap)
        {
            Exp += amount;
            while (Exp > StaticValues.ExpNeededToNextLevel[Level])
            {
                Exp -= StaticValues.ExpNeededToNextLevel[Level];
                LevelUp();
                if (Level == StaticValues.LevelCap)
                {
                    Exp = 0;
                    break;
                }
            }
        }
    }

    public void LevelUp()
    {
        Level++;
        HeroGenerator.LevelUpHero(this);
        // To Do dodać zwiększanie się statystyk bohatera
    }


}
