using UnityEngine;
using UnityEditor;

public class Property
{
    //TODO
    // Implementning this as general counters of data through a game
    // An achievement can then be validated from the properties

    // This should be a complete list of all the properties in the game
    public enum Prop
    {
        HighestScore = 100,
        LongestCloudStreak = 200,
        FirstCloud = 300,
    }

    public Prop EnumName { get; set; }

    public int Value { get; set; }
    
}