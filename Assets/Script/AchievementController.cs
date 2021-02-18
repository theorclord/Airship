using Assets.Script.Achievement;
using Assets.Script.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementController
{
    #region Enums
    // This should be a complete list of all the properties in the game
    public enum Prop
    {
        HighestScore = 100,
        LongestCloudStreak = 200,
        FirstCloud = 300,
    }
    // TODO check if this is necessary to use
    public enum Achieve
    {
        FirstCloud,
        Score500
    }

    public enum AchieveCompareType
    {
        equal, greater, less
    }
    #endregion

    private int streakCount;
    public int StreakCount
    {
        get { return streakCount; }
        set
        {
            if (streakCount > PersistentData.Instance.Properties[Prop.LongestCloudStreak].Value)
            {
                PersistentData.Instance.Properties[Prop.LongestCloudStreak].Value = streakCount;
            }
            streakCount = value;
        }
    }

    public bool InitStreakCount { get; set; }

    public int PersistentedPoints
    {
        get
        {
            return PersistentData.Instance.Properties[Prop.HighestScore].Value;
        }
        set
        {
            if (PersistentData.Instance.Properties[Prop.HighestScore].Value < value)
            {
                UpdateProperty(Prop.HighestScore, value);
            }
        }
    }


    // longest dodge
    // dodgeroo

    //no cactch 
    //longest time no sky or rock
    

    public AchievementController()
    {
        
    }

    
    /// <summary>
    /// This returns all newly valid achievements
    /// </summary>
    /// <param name="propType"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public List<Achievement> UpdateProperty(Prop propType, int value) // 2020-02-18 mist: perhaps change the value to an object to enable more complex properties
    {
        PersistentData.Instance.Properties[propType].Value = value;
        PersistentData.Instance.SaveData(PersistentData.Instance.Properties, Constants.PropertyPath);

        // check all achivements with this property
        var eligibleAch = PersistentData.Instance.Achievements.Where(a => a.Value.PropRelations.FindIndex(prop => prop.PropertyType == propType) >= 0);
        
        List<Achievement> newlyValidAchiements = new List<Achievement>();

        foreach(var ach in eligibleAch)
        {
            // only return the achievements which have just become valid.
            if (ach.Value.UnlockedDate == DateTime.MinValue)
            {
                bool isValid = ach.Value.Validate();
                if (isValid)
                {
                    ach.Value.UnlockedDate = DateTime.Now;
                    PersistentData.Instance.SaveData(PersistentData.Instance.Achievements, Constants.AchievementPath);
                    newlyValidAchiements.Add(ach.Value);
                }
            }
        }
        return newlyValidAchiements;
    }
}
