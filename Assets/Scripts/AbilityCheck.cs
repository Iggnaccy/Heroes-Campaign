using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCheck {
    public Hero TestedHero { get; private set; }
    public int TestDificulty { get; private set; }
    public List<double> StatParticipation { get; private set; }
    public List<double> RaceParticipation { get; private set; }
    public List<double> ClassParticipation { get; private set; }

    public AbilityCheck(Hero testedHero, int testDificulty, List<double> statParticpation, List<double> raceParticipation, List<double> classParticipation)
    {
        TestedHero = testedHero;
        TestDificulty = testDificulty;
        StatParticipation = statParticpation;
        RaceParticipation = raceParticipation;
        ClassParticipation = classParticipation;
    }

    public bool PerformTest()
    {
        double ScoreToBeat=TestDificulty;

        ScoreToBeat -= TestedHero.Stats.Agility * StatParticipation[0];
        ScoreToBeat -= TestedHero.Stats.Health * StatParticipation[1];
        ScoreToBeat -= TestedHero.Stats.Intelligence * StatParticipation[2];
        ScoreToBeat -= TestedHero.Stats.Mana * StatParticipation[3];
        ScoreToBeat -= TestedHero.Stats.Strength * StatParticipation[4];

        ScoreToBeat -= RaceParticipation[(int)(TestedHero.Race)];

        ScoreToBeat -= ClassParticipation[(int)(TestedHero.Profession)];

        return Random.Range(1, 100)>ScoreToBeat;
    }

    public void NextTest(int testDificulty, List<double> statParticpation, List<double> raceParticipation, List<double> classParticipation)
    {
        TestDificulty = testDificulty;
        StatParticipation = statParticpation;
        RaceParticipation = raceParticipation;
        ClassParticipation = classParticipation;
    }

    public void NextHero(Hero testedHero)
    {
        TestedHero = testedHero;
    }

    public void NextHeroAndTest(Hero testedHero, int testDificulty, List<double> statParticpation, List<double> raceParticipation, List<double> classParticipation)
    {
        TestedHero = testedHero;
        TestDificulty = testDificulty;
        StatParticipation = statParticpation;
        RaceParticipation = raceParticipation;
        ClassParticipation = classParticipation;

    }
    
    
    




}
