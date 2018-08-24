using System.Collections;
using System.Collections.Generic;

public class Kingdom
{
    public string Name { get; private set; }
    public int ID { get; private set; }
    public int Chaos { get; set; }
    public string Description { get; private set; }
    public static int ChaosOverwhelming { get;  set; } = 0;
  
    public bool alive;
    public Kingdom(string name, int id, string description)
    {
        alive = true;
        Name = name;
        ID = id;
        Chaos = StaticValues.startingChaos;
        Description = description;
    }

    public void CheckIfAlive()
    {
       if (Chaos < StaticValues.MaxChaos)
            return;
        alive = false;
        ChaosOverwhelming++;
    }

}
