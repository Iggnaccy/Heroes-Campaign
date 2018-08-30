using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MissionStrings
{
    static int Counter;
    public static string GetMissionName()
    {
        string ReturnValue = "TestName " + Counter.ToString();
        Counter++;
        return ReturnValue;
    }

    public static string GetMissionDescription()
    {
        return "TestDescription";
    }



}
