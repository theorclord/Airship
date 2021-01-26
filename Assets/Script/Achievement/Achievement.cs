using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Achievement
{
    public enum Achieve
    {
        FirstCloud
    }

    public enum AchieveCompareType
    {
        equal, greater, less
    }

    private string Name { get; set; } // achievement name -> create from enum
    //TODO add description for achievement
    private Dictionary<AchieveCompareType, Property> Props { get; set; } // array of related properties
    private bool Unlocked { get; set; } // achievement is unlocked or not
 
    public Achievement(string name, Dictionary<AchieveCompareType, Property> requiredProperties)
    {
        Name = name;
        Props = requiredProperties;
        Unlocked = false;
    }
}