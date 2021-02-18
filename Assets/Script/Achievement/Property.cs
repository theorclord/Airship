using UnityEngine;
using UnityEditor;
using System;
using static AchievementController;

/// General counters of data through a game
/// An achievement can then be validated from the properties
[Serializable]
public class Property
{
    public Prop EnumName { get; set; }

    public int Value { get; set; }
    
}