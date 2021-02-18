using System;
using static AchievementController;

namespace Assets.Script.Achievement
{
    [Serializable]
    public class PropertyRelation
    {
        public Prop PropertyType { get; set; }
        public AchieveCompareType CompareType { get; set; }
        public int Threshold { get; set; }
    }
}
