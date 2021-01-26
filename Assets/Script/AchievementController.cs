using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Achievement;

public class AchievementController
{
    private int streakCount;
    public int StreakCount
    {
        get { return streakCount; }
        set
        {
            if (streakCount > PersistentData.Instance.Properties[Property.Prop.LongestCloudStreak].Value)
            {
                PersistentData.Instance.Properties[Property.Prop.LongestCloudStreak].Value = streakCount;
            }
            streakCount = value;
        }
    }

    public bool InitStreakCount { get; set; }

    public int PersistentedPoints
    {
        get
        {
            return PersistentData.Instance.Properties[Property.Prop.HighestScore].Value;
        }
        set
        {
            if (PersistentData.Instance.Properties[Property.Prop.HighestScore].Value < value)
            {
                PersistentData.Instance.Properties[Property.Prop.HighestScore].Value = value;
            }
        }
    }
    
    public bool FirstCloud
    {
        get
        {
            return PersistentData.Instance.Properties[Property.Prop.FirstCloud].Value == 1;
        }
        set
        {
            PersistentData.Instance.Properties[Property.Prop.FirstCloud].Value = value == true ? 1 : 0;
        }
    }

    // longest dodge
    // dodgeroo

    //no cactch 
    //longest time no sky or rock

    public Dictionary<Achieve, Achievement> Achievements { get; set; }

    public AchievementController()
    {
        Achievements = new Dictionary<Achieve, Achievement>
        {
            { Achieve.FirstCloud, new Achievement("First Cloud", new Dictionary<AchieveCompareType, Property>() { { AchieveCompareType.greater, PersistentData.Instance.Properties[Property.Prop.FirstCloud] } }) }
        };
    }
}
