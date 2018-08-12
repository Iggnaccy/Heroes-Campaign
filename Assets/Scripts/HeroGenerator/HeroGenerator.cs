using System;

public class HeroGenerator {

    public static Hero GenerateHero() {
        Random rng = new Random();
        Hero.HeroProfession profession = generateProfession(rng);
        Hero.HeroRace race = generateRace(rng);
        Hero.HeroSex sex = generateSex(rng);
        int age = generateAge(rng, race);
        string name = generateName(rng, race, sex);
        int cost = 100;
        int salary = 20;
        return new Hero(name, profession, race, sex, cost, salary);
    }

    private static Hero.HeroProfession generateProfession(Random rng)
    {
        return Utils.generateRandomEnum<Hero.HeroProfession>(rng);
    }

    private static Hero.HeroRace generateRace(Random rng)
    {
        return Utils.generateRandomEnum<Hero.HeroRace>(rng);
    }

    private static Hero.HeroSex generateSex(Random rng)
    {
        return Utils.generateRandomEnum<Hero.HeroSex>(rng);
    }

    private static int generateAge(Random rng, Hero.HeroRace race) {
        return rng.Next(15, 70);
    }

    //to do
    private static string generateName(Random rng, Hero.HeroRace race, Hero.HeroSex sex) {
        return "HAL-9000";
    }
}
