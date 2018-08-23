using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class HeroGenerator
{

    private static System.Random rng;

    public static Hero GenerateHero(System.Random random)
    {
        rng = random;
        Hero.HeroProfession profession = GenerateProfession();
        Hero.HeroRace race = GenerateRace();
        Hero.HeroSex sex = GenerateSex();
        int age = GenerateAge(race);
        int level = GenerateLevel();
        Hero.HeroStats stats = GenerateStats(profession, race, sex, age, level);
        string name = GenerateName(race, sex);
        int cost = 100;
        int salary = 20;
        return new Hero(name, profession, race, sex, stats, level, cost, salary, age);
    }

    private static Hero.HeroProfession GenerateProfession()
    {
        return Utils.generateRandomEnum<Hero.HeroProfession>(rng);
    }

    private static Hero.HeroRace GenerateRace()
    {
        return Utils.generateRandomEnum<Hero.HeroRace>(rng);
    }

    private static Hero.HeroSex GenerateSex()
    {
        return Utils.generateRandomEnum<Hero.HeroSex>(rng);
    }

    private static int GenerateAge(Hero.HeroRace race)
    {
        return rng.Next(15, 70);
    }

    private static int GenerateLevel()
    {
        return rng.Next(StaticValues.startingLevel, StaticValues.ExpNeededToNextLevel.GetLength(0) + 1);
    }

    private static Hero.HeroStats GenerateStats(Hero.HeroProfession profession, Hero.HeroRace race,
        Hero.HeroSex sex, int age, int level)
    {
        //int agility, int health, int intelligence, int mana, int strength
        Dictionary<Hero.HeroProfession, Hero.HeroStats> profMap = new Dictionary<Hero.HeroProfession, Hero.HeroStats>{
            { Hero.HeroProfession.Archer,  new Hero.HeroStats  (10, 0, 0, 0, 4) },
            { Hero.HeroProfession.Druid,   new Hero.HeroStats  (0, 2, 10, 10, 0) },
            { Hero.HeroProfession.Intern,  new Hero.HeroStats  (0, 0, 0, 0, 0) },
            { Hero.HeroProfession.Knight,  new Hero.HeroStats  (2, 10, 5, 0, 10) },
            { Hero.HeroProfession.Mage,    new Hero.HeroStats  (0, 0, 15, 15, 0) },
            { Hero.HeroProfession.Rogue,   new Hero.HeroStats  (15, 0, 10, 0, 5) },
            { Hero.HeroProfession.Warrior, new Hero.HeroStats  (0, 10, 0, 0, 15) }
        };
        Assert.IsTrue(profMap.Count == Utils.getEnumLength<Hero.HeroProfession>());

        Dictionary<Hero.HeroRace, Hero.HeroStats> raceMap = new Dictionary<Hero.HeroRace, Hero.HeroStats>{
            { Hero.HeroRace.Djin,     new Hero.HeroStats  (10, 0, 10, 0, 0) },
            { Hero.HeroRace.Dwarf,    new Hero.HeroStats  (0, 10, 0, 0, 20) },
            { Hero.HeroRace.Elf,      new Hero.HeroStats  (20, 0, 5, 5, 0) },
            { Hero.HeroRace.Gnome,    new Hero.HeroStats  (0, 0, 20, 0, 0) },
            { Hero.HeroRace.Goblin,   new Hero.HeroStats  (10, 0, 0, 0, 0) },
            { Hero.HeroRace.Human,    new Hero.HeroStats  (0, 0, 10, 0, 0) },
            { Hero.HeroRace.Orc,      new Hero.HeroStats  (0, 0, 0, 0, 15) },
            { Hero.HeroRace.Skeletor, new Hero.HeroStats  (0, 0, 0, 0, 10) },
            { Hero.HeroRace.Troll,    new Hero.HeroStats  (0, 0, -20, 0, 25) },
            { Hero.HeroRace.Vampire,  new Hero.HeroStats  (0, 10, 20, 10, 0) }
        };
        Assert.IsTrue(raceMap.Count == Utils.getEnumLength<Hero.HeroRace>());

        Dictionary<Hero.HeroSex, Hero.HeroStats> sexMap = new Dictionary<Hero.HeroSex, Hero.HeroStats> {
            { Hero.HeroSex.Female, new Hero.HeroStats (5, 0, 0, 0, 0)},
            { Hero.HeroSex.Male,   new Hero.HeroStats (0, 0, 0, 0, 10)}
        };

        Hero.HeroStats stats = new Hero.HeroStats();
        Hero.HeroStats tickets = new Hero.HeroStats(StaticValues.initialHeroStats);
        tickets += profMap[profession];
        tickets += raceMap[race];
        tickets += sexMap[sex];
        tickets.ClampToZero();

        List<KeyValuePair<int, Hero.HeroStats>> helper = new List<KeyValuePair<int, Hero.HeroStats>> {
            new KeyValuePair<int, Hero.HeroStats>(tickets.Agility,      new Hero.HeroStats(1,0,0,0,0)),
            new KeyValuePair<int, Hero.HeroStats>(tickets.Health,       new Hero.HeroStats(0,1,0,0,0)),
            new KeyValuePair<int, Hero.HeroStats>(tickets.Intelligence, new Hero.HeroStats(0,0,1,0,0)),
            new KeyValuePair<int, Hero.HeroStats>(tickets.Mana,         new Hero.HeroStats(0,0,0,1,0)),
            new KeyValuePair<int, Hero.HeroStats>(tickets.Strength,     new Hero.HeroStats(0,0,0,0,1)),
        };

        Assert.IsTrue(helper.Count == Hero.HeroStats.ParamsCount());
        int total = 0;
        for (int i = 0; i < helper.Count; i++)
            total += helper[i].Key;

        List<KeyValuePair<List<int>, Hero.HeroStats>> rangesMapping = new List<KeyValuePair<List<int>, Hero.HeroStats>>();
        int left = 0, right = 0;
        for (int i = 0; i < helper.Count; i++)
        {
            right += helper[i].Key;
            rangesMapping.Add(new KeyValuePair<List<int>, Hero.HeroStats>(
                new List<int> { left, right },
                helper[i].Value));
            left = right;
        }

        int points = level * StaticValues.pointsPerLevel;
        for (int i = 0; i < points; i++)
        {
            int r = rng.Next() % total;
            bool found = false;
            for (int j = 0; j < rangesMapping.Count; j++)
            {
                if (r >= rangesMapping[j].Key[0] && r <= rangesMapping[j].Key[1])
                {
                    stats += rangesMapping[j].Value;
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }
        return stats;
    }

    public static void LevelUpHero(Hero h)
    {
        Hero.HeroStats gainedSkills = GenerateStats(h.Profession, h.Race, h.Sex, h.Age, 1);
        h.Stats += gainedSkills;
    }

    //to do
    private static string GenerateName(Hero.HeroRace race, Hero.HeroSex sex)
    {
        List<string> nordicFemale = (Resources.Load("HeroNames/NordicFemale") as TextAsset).text.Split('\n').ToList();
        List<string> nordicMale = (Resources.Load("HeroNames/NordicMale") as TextAsset).text.Split('\n').ToList();
        List<string> slavicFemale = (Resources.Load("HeroNames/SlavicFemale") as TextAsset).text.Split('\n').ToList();
        List<string> slavicMale = (Resources.Load("HeroNames/SlavicMale") as TextAsset).text.Split('\n').ToList();
        List<string> orcFemale = (Resources.Load("HeroNames/OrcFemale") as TextAsset).text.Split('\n').ToList();
        List<string> orcMale = (Resources.Load("HeroNames/OrcMale") as TextAsset).text.Split('\n').ToList();
        List<string> vampireFemale = (Resources.Load("HeroNames/VampireFemale") as TextAsset).text.Split('\n').ToList();
        List<string> vampireMale = (Resources.Load("HeroNames/VampireMale") as TextAsset).text.Split('\n').ToList();
        List<string> elfFemale = (Resources.Load("HeroNames/ElfFemale") as TextAsset).text.Split('\n').ToList();
        List<string> elfMale = (Resources.Load("HeroNames/ElfMale") as TextAsset).text.Split('\n').ToList();
        List<string> djin = (Resources.Load("HeroNames/Djin") as TextAsset).text.Split('\n').ToList();

        Dictionary<char, List<string>> nameEncoding = new Dictionary<char, List<string>> {
            { 'n', nordicFemale },
            { 'N', nordicMale },
            { 's', slavicFemale },
            { 'S', slavicMale},
            { 'o', orcFemale},
            { 'O', orcMale},
            { 'v', vampireFemale},
            { 'V', vampireMale},
            { 'e', elfFemale},
            { 'E', elfMale},
            { 'd', djin},
            { 'D', djin},
        };

        Dictionary<Hero.HeroRace, string> nameMapping = new Dictionary<Hero.HeroRace, string>() {
            { Hero.HeroRace.Djin, "dD"},
            { Hero.HeroRace.Dwarf, "nN"},
            { Hero.HeroRace.Elf, "eE"},
            { Hero.HeroRace.Gnome, "oO"},
            { Hero.HeroRace.Goblin, "oO"},
            { Hero.HeroRace.Human, "nNsS"},
            { Hero.HeroRace.Orc, "oO"},
            { Hero.HeroRace.Skeletor, "vV"},
            { Hero.HeroRace.Troll, "oO"},
            { Hero.HeroRace.Vampire, "vV"}
        };

        Assert.IsTrue(nameMapping.Count == Utils.getEnumLength<Hero.HeroRace>());

        string name = "ENDLESS_NAMELESS";
        List<List<string>> listCandidates = new List<List<string>>();
        foreach (char c in nameMapping[race])
        {
            if (c >= 'a' && c <= 'z' && sex == Hero.HeroSex.Female)
                listCandidates.Add(nameEncoding[c]);
            if (c >= 'A' && c <= 'Z' && sex == Hero.HeroSex.Male)
                listCandidates.Add(nameEncoding[c]);
        }
        if (listCandidates.Count != 0)
            name = Utils.markowNameGenerator(listCandidates[StaticValues.rng.Next() % listCandidates.Count], 2, 3, 10);

        return name;
        // return Utils.markowNameGenerator(nordicMaleNames, 2, 3, 10);
    }
}