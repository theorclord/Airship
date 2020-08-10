using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController
{
    // TODO propertyfy the streak
    private int streakCount;
    public int StreakCount
    {
        get { return streakCount; }
        set
        {
            if (streakCount > LongestStreak)
            {
                LongestStreak = streakCount;
            }
            streakCount = value;
        }
    }
    public bool InitStreakCount { get; set; }
    public int LongestStreak
    {
        get;
        private set;
    }


    public int Points
    {
        get
        {
            return Properties[Property.Prop.Score].Value;
        }
        set
        {
            Properties[Property.Prop.Score].Value = value;
        }
    }
    
    // TODO split property and achievement part
    public bool FirstCloud
    {
        get
        {
            return Properties[Property.Prop.FirstCloud].Value == 1;
        }
        set
        {
            Properties[Property.Prop.Score].Value = value == true ? 1 : 0;
        }
    }

    // longest dodge
    // dodgeroo

    //no cactch 
    //longest time no sky or rock

    public Dictionary<Property.Prop, Property> Properties { get; private set; }

    public AchievementController()
    {
        //TODO load properties from persisted data.
        Properties = new Dictionary<Property.Prop,Property>();
        var scoreProp = new Property()
        {
            EnumName = Property.Prop.Score,
            Value = 0, // should be the persisted data
        };
        Properties.Add(Property.Prop.Score, scoreProp);
        var firstProp = new Property()
        {
            EnumName = Property.Prop.FirstCloud,
            Value = 0, // should be the persisted data
        };
        Properties.Add(Property.Prop.FirstCloud, firstProp);
    }
}
