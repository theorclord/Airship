using Assets.Script.Achievement;
using Assets.Script.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static AchievementController;

public class PersistentData : MonoBehaviour
{
    #region IngamgeVariables
    public bool Pause { get; set; }
    public HighScoreEntry CurrentHighscoreEntry { get; set; }
    #endregion

    #region BinaryPersistence
    // the two lists are saved in a common object on serialization to only have one object
    public List<HighScoreEntry> HighscoreEntriesTime { get; set; }
    public List<HighScoreEntry> HighscoreEntriesScore { get; set; }
    
    public Dictionary<Prop, Property> Properties
    {
        get;
        private set;
    }

    public Dictionary<Achieve, Achievement> Achievements { get; set; }
    #endregion

    #region PlayerPrefs
    private float soundValPrivate;
    public float SoundVal {
        get
        {
            return soundValPrivate;
        }
        set
        {
            soundValPrivate = value;
            PlayerPrefs.SetFloat(nameof(SoundVal), value);
            PlayerPrefs.Save();
        }
    }
    #endregion

    private AchievementController _achievementController;
    public AchievementController AchievementController
    {
        get
        {
            if (_achievementController == null)
            {
                _achievementController = new AchievementController();
            }
            return _achievementController;
        }
    }

    public static PersistentData Instance { get; set; }
    void Awake()
    {
        //If we don't currently have a game control...
        if (Instance == null)
        {
            //...set this one to be it...
            Instance = this;
        }
        //...otherwise...
        else if (Instance != this)
        {
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        // Load the soundval from last config file.
        SoundVal = PlayerPrefs.GetFloat(nameof(SoundVal), 1);
        
        LoadHighscoreData();
        LoadPropertyData();
        LoadAchievementData();

        _achievementController = new AchievementController();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveData(object DataCollection, string path)
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + path, FileMode.OpenOrCreate, FileAccess.Write);
        binFormatter.Serialize(stream, DataCollection);
        stream.Close();
    }

    #region HighScore
    public void AddHighscoreEntry(HighScoreEntry highscoreEntry)
    {
        // handle the score addition
        if(HighscoreEntriesScore.Count < Constants.NumberHighscoreEntries)
        {
            HighscoreEntriesScore.Add(highscoreEntry);
            HighscoreEntriesScore = HighscoreEntriesScore.OrderByDescending(h => h.Score).ToList();
        } else
        {
            var lowerEntryExist = HighscoreEntriesScore.Last().Score < highscoreEntry.Score;
            if(lowerEntryExist)
            {
                HighscoreEntriesScore.Add(highscoreEntry);
                HighscoreEntriesScore = HighscoreEntriesScore.OrderByDescending(h => h.Score).ToList();
                HighscoreEntriesScore.Remove(HighscoreEntriesScore.Last());
            }
        }

        // Handle the time addition
        if (HighscoreEntriesTime.Count < Constants.NumberHighscoreEntries)
        {
            HighscoreEntriesTime.Add(highscoreEntry);
            HighscoreEntriesTime = HighscoreEntriesTime.OrderByDescending(h => h.Time).ToList();
        }
        else
        {
            var lowerEntryExist = HighscoreEntriesTime.Last().Time < highscoreEntry.Time;
            if (lowerEntryExist)
            {
                HighscoreEntriesTime.Add(highscoreEntry);
                HighscoreEntriesTime = HighscoreEntriesTime.OrderByDescending(h => h.Time).ToList();
                HighscoreEntriesTime.Remove(HighscoreEntriesTime.Last());
            }
        }
    }

    private void LoadHighscoreData()
    {
        List<List<HighScoreEntry>> highScores = new List<List<HighScoreEntry>>() { new List<HighScoreEntry>(), new List<HighScoreEntry>() };
        
        try
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + Constants.HighscorePath, FileMode.OpenOrCreate, FileAccess.Read);
            object loadedObj = null;
            if (stream.Length > 0)
            {
                loadedObj = binFormatter.Deserialize(stream);
            }
            stream.Close();
            highScores = (List<List<HighScoreEntry>>)loadedObj;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("Unable to parse saved highscores");
        }
        
        var HighScoreLists = highScores;
        HighscoreEntriesScore = HighScoreLists[0];
        HighscoreEntriesTime = HighScoreLists[1];
    }

    public void SaveHighscoreData()
    {
        List<List<HighScoreEntry>> highScores = new List<List<HighScoreEntry>>() { HighscoreEntriesScore, HighscoreEntriesTime };
        SaveData(highScores, Constants.HighscorePath);
    }

    public void ClearHighScore()
    {
        HighscoreEntriesScore = new List<HighScoreEntry>();
        HighscoreEntriesTime = new List<HighScoreEntry>();
        SaveHighscoreData();
        LoadHighscoreData();
    }
    #endregion

    #region PropertyData
    private void LoadPropertyData()
    {
        var loadedProperties = new Dictionary<Prop, Property>();
        // load the properties
        try
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + Constants.PropertyPath, FileMode.OpenOrCreate, FileAccess.Read);
            object loadedObj = null;
            if (stream.Length > 0)
            {
                loadedObj = binFormatter.Deserialize(stream);
            }
            stream.Close();
            loadedProperties = loadedObj != null ? (Dictionary<Prop, Property>)loadedObj : new Dictionary<Prop, Property>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("Unable to parse existing properties");
        }
        
        // ensure properties exist.
        Properties = new Dictionary<Prop, Property>()
        {
            {
                Prop.FirstCloud,
                new Property()
                {
                    EnumName = Prop.FirstCloud,
                    Value = loadedProperties.ContainsKey(Prop.FirstCloud) ? loadedProperties[Prop.FirstCloud].Value : 0,
                }
            },
            {
                Prop.CurrentScore,
                new Property()
                {
                    EnumName = Prop.CurrentScore,
                    Value = loadedProperties.ContainsKey(Prop.CurrentScore) ? loadedProperties[Prop.CurrentScore].Value : 0,
                }
            },
            {
                Prop.CurrentCloudStreak,
                new Property()
                {
                    EnumName = Prop.CurrentCloudStreak,
                    Value = loadedProperties.ContainsKey(Prop.CurrentCloudStreak) ? loadedProperties[Prop.CurrentCloudStreak].Value : 0,
                }
            },
            {
                Prop.AllTimeScore,
                new Property()
                {
                    EnumName = Prop.AllTimeScore,
                    Value = loadedProperties.ContainsKey(Prop.AllTimeScore) ? loadedProperties[Prop.AllTimeScore].Value : 0,
                }
            },
            {
                Prop.RocksHit,
                new Property()
                {
                    EnumName = Prop.RocksHit,
                    Value = loadedProperties.ContainsKey(Prop.RocksHit) ? loadedProperties[Prop.RocksHit].Value : 0,
                }
            },
            {
                Prop.CloudsMissed,
                new Property()
                {
                    EnumName = Prop.CloudsMissed,
                    Value = loadedProperties.ContainsKey(Prop.CloudsMissed) ? loadedProperties[Prop.CloudsMissed].Value : 0,
                }
            },
            {
                Prop.CurrentSpeed,
                new Property()
                {
                    EnumName = Prop.CurrentSpeed,
                    Value = loadedProperties.ContainsKey(Prop.CurrentSpeed) ? loadedProperties[Prop.CurrentSpeed].Value : 0,
                }
            },
        };
    }
    #endregion

    #region Achievements
    private void LoadAchievementData()
    {
        var loadedAchievements = new Dictionary<Achieve, Achievement>();
        // load the properties
        try
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + Constants.AchievementPath, FileMode.OpenOrCreate, FileAccess.Read);
            object loadedObj = null;
            if (stream.Length > 0)
            {
                loadedObj = binFormatter.Deserialize(stream);
            }
            stream.Close();
            loadedAchievements = loadedObj != null ? (Dictionary<Achieve, Achievement>)loadedObj : new Dictionary<Achieve, Achievement>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("Unable to parse existing achievements");
        }

        // ensure achievements exist.
        Achievements = new Dictionary<Achieve, Achievement>()
        {
            {
                Achieve.FirstCloud,
                new Achievement()
                {
                    Name = "First Cloud",
                    Description = "Starting out small. Catch your first cloud",
                    PropRelations = new List<PropertyRelation>() {
                        new PropertyRelation()
                        {
                            PropertyType = Prop.FirstCloud,
                            CompareType = AchieveCompareType.Equal,
                            Threshold = 1
                        }
                    },
                    SpriteName = "FirstCloudAchievement",
                    UnlockedDate = loadedAchievements.ContainsKey(Achieve.FirstCloud) ? loadedAchievements[Achieve.FirstCloud].UnlockedDate : DateTime.MinValue,
                }
            },
            {
                Achieve.Score500,
                new Achievement()
                {
                    Name = "First flight",
                    Description = "This is becoming a small business adventure. Score 500 points",
                    PropRelations = new List<PropertyRelation>() {
                        new PropertyRelation()
                        {
                            PropertyType = Prop.CurrentScore,
                            CompareType = AchieveCompareType.GreaterOrEqual,
                            Threshold = 500
                        }
                    },
                    SpriteName = "Cloud500Achievement",
                    UnlockedDate = loadedAchievements.ContainsKey(Achieve.Score500) ? loadedAchievements[Achieve.Score500].UnlockedDate : DateTime.MinValue,
                }
            },
            {
                Achieve.AllTime10000,
                new Achievement()
                {
                    Name = "Breaking 10000",
                    Description = "Try and try again. Score 10000 points, can be done over multiple playthroughs",
                    PropRelations = new List<PropertyRelation>() {
                        new PropertyRelation()
                        {
                            PropertyType = Prop.AllTimeScore,
                            CompareType = AchieveCompareType.GreaterOrEqual,
                            Threshold = 10000
                        }
                    },
                    SpriteName = "AllTime10000Achievement", // TODO create sprite 
                    UnlockedDate = loadedAchievements.ContainsKey(Achieve.AllTime10000) ? loadedAchievements[Achieve.AllTime10000].UnlockedDate : DateTime.MinValue,
                }
            },
            {
                Achieve.StreakCloud5,
                new Achievement()
                {
                    Name = "Warming up",
                    Description = "Do not let any cloud go to waste. Collect 5 clouds in a row",
                    PropRelations = new List<PropertyRelation>() {
                        new PropertyRelation()
                        {
                            PropertyType = Prop.CurrentCloudStreak,
                            CompareType = AchieveCompareType.GreaterOrEqual,
                            Threshold = 5
                        }
                    },
                    SpriteName = "Streak5Achievement",
                    UnlockedDate = loadedAchievements.ContainsKey(Achieve.StreakCloud5) ? loadedAchievements[Achieve.StreakCloud5].UnlockedDate : DateTime.MinValue,
                }
            },
            {
                Achieve.MaxSpeed,
                new Achievement()
                {
                    Name = "Full Throttle",
                    Description = "This one goes to 11. Reach max speed",
                    PropRelations = new List<PropertyRelation>() {
                        new PropertyRelation()
                        {
                            PropertyType = Prop.CurrentSpeed,
                            CompareType = AchieveCompareType.Equal,
                            Threshold = (int)Constants.MaxSpeed+1
                        }
                    },
                    SpriteName = "MaxSpeedAchievement",
                    UnlockedDate = loadedAchievements.ContainsKey(Achieve.MaxSpeed) ? loadedAchievements[Achieve.MaxSpeed].UnlockedDate : DateTime.MinValue,
                }
            },
        };
    }
    #endregion
}
