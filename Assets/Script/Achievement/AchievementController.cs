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
        CurrentScore = 100,
        CurrentCloudStreak = 200,
        FirstCloud = 300, // TODO: This seems redundant
        AllTimeScore = 400,
        CloudsMissed = 500,
        RocksHit = 600,
        CurrentSpeed = 700,
    }
    
    public enum Achieve
    {
        FirstCloud,
        Score500,
        AllTime10000,
        StreakCloud5,
        MaxSpeed,
    }

    public enum AchieveCompareType
    {
        Equal, Greater, Less, GreaterOrEqual
    }
    #endregion
    
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
