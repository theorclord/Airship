using System;

[Serializable]
public class HighscoreEntry
{
    public string Name { get; set; }
    public int Score { get; set; }
    public float Time { get; set; }
    public DateTime Date { get; set; }
}