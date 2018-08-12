using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Assertions;

public class HeroGenerator {

    private static Random rng;

    public static Hero GenerateHero(Random random) {
        rng = random;
        Hero.HeroProfession profession = generateProfession();
        Hero.HeroRace race = generateRace();
        Hero.HeroSex sex = generateSex();
        int age = generateAge(race);
        int level = generateLevel();
        Hero.HeroStats stats = generateStats(profession, race, sex, age, level);
        string name = generateName(race, sex);
        int cost = 100;
        int salary = 20;
        return new Hero(name, profession, race, sex, stats, level, cost, salary);
    }

    private static Hero.HeroProfession generateProfession()
    {
        return Utils.generateRandomEnum<Hero.HeroProfession>(rng);
    }

    private static Hero.HeroRace generateRace()
    {
        return Utils.generateRandomEnum<Hero.HeroRace>(rng);
    }

    private static Hero.HeroSex generateSex()
    {
        return Utils.generateRandomEnum<Hero.HeroSex>(rng);
    }

    private static int generateAge(Hero.HeroRace race) {
        return rng.Next(15, 70);
    }

    private static int generateLevel() {
        return rng.Next(StaticValues.startingLevel, StaticValues.ExpNeededToNextLevel.GetLength(0)+1);
    }

    private static Hero.HeroStats generateStats(Hero.HeroProfession profession, Hero.HeroRace race,
        Hero.HeroSex sex, int age, int level) {
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
            new KeyValuePair<int, Hero.HeroStats>(tickets.agility,      new Hero.HeroStats(1,0,0,0,0)),
            new KeyValuePair<int, Hero.HeroStats>(tickets.health,       new Hero.HeroStats(0,1,0,0,0)),
            new KeyValuePair<int, Hero.HeroStats>(tickets.intelligence, new Hero.HeroStats(0,0,1,0,0)),
            new KeyValuePair<int, Hero.HeroStats>(tickets.mana,         new Hero.HeroStats(0,0,0,1,0)),
            new KeyValuePair<int, Hero.HeroStats>(tickets.strength,     new Hero.HeroStats(0,0,0,0,1)),
        };

        Assert.IsTrue(helper.Count == Hero.HeroStats.paramsCount());
        int total = 0;
        for (int i = 0; i < helper.Count; i++)
            total += helper[i].Key;

        List<KeyValuePair<List<int>, Hero.HeroStats > > rangesMapping = new List<KeyValuePair<List<int>, Hero.HeroStats>>();
        int left = 0, right = 0;
        for (int i = 0; i < helper.Count; i++) {
            right += helper[i].Key;
            rangesMapping.Add(new KeyValuePair<List<int>, Hero.HeroStats>(
                new List<int> { left, right},
                helper[i].Value));
            left = right;
        }

        int points = level * StaticValues.pointsPerLevel;
        for (int i = 0; i < points; i++) {
            int r = rng.Next() % total;
            bool found = false;
            for (int j = 0; j < rangesMapping.Count; j++) {
                if (r >= rangesMapping[j].Key[0] && r <= rangesMapping[j].Key[1]) {
                    stats += rangesMapping[j].Value;
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }
        return stats;
    }

    //to do
    private static string generateName(Hero.HeroRace race, Hero.HeroSex sex) {
        return "HAL-9000";
    }
}
