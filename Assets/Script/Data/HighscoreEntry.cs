using System;

[Serializable]
public class HighScoreEntry
{
    public string Name { get; set; }
    public int Score { get; set; }
    public float Time { get; set; }
    public DateTime Date { get; set; }
}