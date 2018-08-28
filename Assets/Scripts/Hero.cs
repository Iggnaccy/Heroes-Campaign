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
        public int Agility { get; set; }
        public int Health { get; set; }
        public int Intelligence { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }

        //TODO: change this to some generics kung-fu
        public static int ParamsCount() {
            return 5;
        }

        public HeroStats() { }

        public HeroStats(int initialValue) {
            Agility = initialValue;
            Health = initialValue;
            Intelligence = initialValue;
            Mana = initialValue;
            Strength = initialValue;
        }

        public HeroStats(int _agility, int _health, int _intelligence, int _mana, int _strength) {
            Agility = _agility;
            Health = _health;
            Intelligence = _intelligence;
            Mana = _mana;
            Strength = _strength;
        }

        public static HeroStats operator +(HeroStats left, HeroStats right)
        {
            left.Agility += right.Agility;
            left.Health += right.Health;
            left.Intelligence += right.Intelligence;
            left.Mana += right.Mana;
            left.Strength += right.Strength;
            return left;
        }

        public void ClampToZero() {
            Agility = Mathf.Max(Agility, 0);
            Health = Mathf.Max(Health, 0);
            Intelligence = Mathf.Max(Intelligence, 0);
            Mana = Mathf.Max(Mana, 0);
            Strength = Mathf.Max(Strength, 0);
        }

        public void Log() {
            Debug.Log(string.Format("Agility: {0}, Health: {1}, Intelligence: {2}, Mana: {3}, Strength: {4}",
                Agility, Health, Intelligence, Mana, Strength));
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
    public Sprite Portrait { get; set; }

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
        Portrait = Cache.GetPortrait(Sex, Race, Profession);
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
