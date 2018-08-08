using System.Collections;
using System.Collections.Generic;

public class Kingdom
{
    public string Name { get; private set; }
    public int ID { get; private set; }
    public int Chaos { get; set; }
    public string Description { get; private set; }

    public Kingdom(string name, int id, string description)
    {
        Name = name;
        ID = id;
        Chaos = StaticValues.startingChaos;
        Description = description;
    }
}
