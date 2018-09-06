using System.Collections;
using System.Collections.Generic;

public class Kingdom
{
    public string Name { get; private set; }
    public int ID { get; private set; }
    public int Chaos { get; set; }
    public string Description { get; private set; }
    public static int ChaosOverwhelming { get;  set; } = 0;
  
    public bool available;

    public Kingdom(string name, int id, string description)
    {
        available = true;
        Name = name;
        ID = id;
        Chaos = StaticValues.startingChaos;
        Description = description;
    }

    public bool IsAvailable()
    {
        return Chaos < StaticValues.MaxChaos;
    }

    public bool IsCleansed() {
        return Chaos <= StaticValues.ChaosLevelForCleansedKingdom;
    }

    public void SetKingdomLocked(bool locked) {
        available = !locked;
        if (locked)
            ChaosOverwhelming++;
        else
            ChaosOverwhelming--;
    }
}
