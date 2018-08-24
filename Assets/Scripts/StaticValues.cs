using System;
using System.Collections;
using System.Collections.Generic;

public static class StaticValues
{
    public static int startingChaos = 25;
    public static int MaxChaos = 100;
    public static int MaxChaosOverwhelming = 3;
    public static int startingExp = 0;
    public static int startingLevel = 0;
    public static int LevelCap = 15;
    public static int initialHeroStats = 20;
    public static int pointsPerLevel = 10;
    public static int[] ExpNeededToNextLevel = { 100, 200, 300, 500, 700, 1000, 1500, 2000, 3000, 4500, 7000, 10000, 15000, 25000, 45000 };
    public static int NumberOfKIngdoms = 6;

    public static Random rng = new Random();
}