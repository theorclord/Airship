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


    public void SaveHighscoreData()
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + Constants.HighscorePath, FileMode.OpenOrCreate, FileAccess.Write);
        List<List<HighScoreEntry>> highScores = new List<List<HighScoreEntry>>() { HighscoreEntriesScore, HighscoreEntriesTime };
        binFormatter.Serialize(stream, highScores);
        stream.Close();
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

    public void ClearHighScore()
    {
        HighscoreEntriesScore = new List<HighScoreEntry>();
        HighscoreEntriesTime = new List<HighScoreEntry>();
        SaveHighscoreData();
        LoadHighscoreData();
    }
    #endregion

    #region PropertyData
    public void SavePropertyData()
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + Constants.PropertyPath, FileMode.OpenOrCreate, FileAccess.Write);
        binFormatter.Serialize(stream, Properties);
        stream.Close();
    }

    private void LoadPropertyData()
    {
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
            Properties = loadedObj != null ? (Dictionary<Prop, Property>)loadedObj : new Dictionary<Prop, Property>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("Unable to parse existing properties");
        }

        // ensure properties exist.
        if (!Properties.ContainsKey(Prop.FirstCloud))
        {
            var firstProp = new Property()
            {
                EnumName = Prop.FirstCloud,
                Value = 0,
            };
            Properties.Add(Prop.FirstCloud, firstProp);
        }
        if(!Properties.ContainsKey(Prop.HighestScore))
        {
            var scoreProp = new Property()
            {
                EnumName = Prop.HighestScore,
                Value = 0,
            };
            Properties.Add(Prop.HighestScore, scoreProp);
        }
        if (!Properties.ContainsKey(Prop.LongestCloudStreak))
        {
            var streakProp = new Property()
            {
                EnumName = Prop.LongestCloudStreak,
                Value = 0,
            };
            Properties.Add(Prop.LongestCloudStreak, streakProp);
        }
    }
    #endregion

    public void SaveAchievementData()
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + Constants.AchievementPath, FileMode.OpenOrCreate, FileAccess.Write);
        binFormatter.Serialize(stream, Achievements);
        stream.Close();
    }

    private void LoadAchievementData()
    {
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
            Achievements = loadedObj != null ? (Dictionary<Achieve, Achievement>)loadedObj : new Dictionary<Achieve, Achievement>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log("Unable to parse existing achievements");
        }

        // ensure achievements exist.
        if (!Achievements.ContainsKey(Achieve.FirstCloud))
        {
            Achievements.Add(
                Achieve.FirstCloud,
                new Achievement("First Cloud",
                    new List<PropertyRelation>() {
                        new PropertyRelation()
                        {
                            PropertyType = Prop.FirstCloud,
                            CompareType = AchieveCompareType.equal,
                            Threshold = 1
                        }
                    }
                )
            );
        }
    }
}
