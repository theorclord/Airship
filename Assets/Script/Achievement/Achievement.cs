using System.Collections.Generic;
using System;
using Assets.Script.Achievement;
using static AchievementController;

[Serializable]
public class Achievement
{
    public string Name { get; set; }
    //TODO add description for achievement
    public List<PropertyRelation> PropRelations { get; set; } // array of related properties
    public DateTime UnlockedDate { get; set; }
    public string SpriteName { get; set; }

    public Achievement() { UnlockedDate = DateTime.MinValue; }

    // todo remove
    public Achievement(string name, List<PropertyRelation> requiredProperties, string spriteName)
    {
        Name = name;
        PropRelations = requiredProperties;
        UnlockedDate = DateTime.MinValue;
        SpriteName = spriteName;
    }

    public bool Validate()
    {
        var isValid = false;

        foreach(var prop in PropRelations)
        {
            switch (prop.CompareType)
            {
                case AchieveCompareType.equal:
                    isValid = prop.Threshold == PersistentData.Instance.Properties[prop.PropertyType].Value;
                    break;
                case AchieveCompareType.greater:
                    isValid = prop.Threshold < PersistentData.Instance.Properties[prop.PropertyType].Value;
                    break;
                case AchieveCompareType.less:
                    isValid = prop.Threshold > PersistentData.Instance.Properties[prop.PropertyType].Value;
                    break;
                default:
                    // in case new comparison methods are added.
                    isValid = false;
                    break;
            }
        }

        return isValid;
    }
}