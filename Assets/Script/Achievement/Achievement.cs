using System.Collections.Generic;
using System;
using Assets.Script.Achievement;
using static AchievementController;

[Serializable]
public class Achievement
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<PropertyRelation> PropRelations { get; set; } // array of related properties
    public DateTime UnlockedDate { get; set; }
    public string SpriteName { get; set; }

    public Achievement() { UnlockedDate = DateTime.MinValue; }

    public bool Validate()
    {
        var isValid = false;

        foreach(var prop in PropRelations)
        {
            switch (prop.CompareType)
            {
                case AchieveCompareType.Equal:
                    isValid = prop.Threshold == PersistentData.Instance.Properties[prop.PropertyType].Value;
                    break;
                case AchieveCompareType.Greater:
                    isValid = prop.Threshold < PersistentData.Instance.Properties[prop.PropertyType].Value;
                    break;
                case AchieveCompareType.Less:
                    isValid = prop.Threshold > PersistentData.Instance.Properties[prop.PropertyType].Value;
                    break;
                case AchieveCompareType.GreaterOrEqual:
                    isValid = prop.Threshold <= PersistentData.Instance.Properties[prop.PropertyType].Value;
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