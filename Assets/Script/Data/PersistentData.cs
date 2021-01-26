using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    #region IngamgeVariables
    public bool Pause { get; set; }
    public HighscoreEntry CurrentHighscoreEntry { get; set; }
    #endregion

    #region BinaryPersistence
    public List<HighscoreEntry> HighScoreList { get; set; }

    private Dictionary<Property.Prop, Property> _properties;
    public Dictionary<Property.Prop, Property> Properties
    {
        get
        {
            if (_properties == null)
            {
                LoadPropertyData();
            }
            return _properties;
        }
    }
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

    public void SaveHighscoreData()
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + @"\highscore.bin", FileMode.OpenOrCreate, FileAccess.Write);
        binFormatter.Serialize(stream, HighScoreList);
        stream.Close();
    }

    private void LoadHighscoreData()
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + @"\highscore.bin", FileMode.OpenOrCreate, FileAccess.Read);
        object loadedObj = null;
        if(stream.Length > 0)
        {
            loadedObj = binFormatter.Deserialize(stream);
        }
        stream.Close();
        
        HighScoreList = loadedObj != null ? (List<HighscoreEntry>)loadedObj : new List<HighscoreEntry>();
    }

    private void SavePropertyData()
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + @"\Property.bin", FileMode.OpenOrCreate, FileAccess.Write);
        binFormatter.Serialize(stream, Properties);
        stream.Close();
    }

    private void LoadPropertyData()
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + @"\Property.bin", FileMode.OpenOrCreate, FileAccess.Read);
        object loadedObj = null;
        if (stream.Length > 0)
        {
            loadedObj = binFormatter.Deserialize(stream);
        }
        stream.Close();
        _properties = loadedObj != null ? (Dictionary<Property.Prop, Property>)loadedObj : new Dictionary<Property.Prop, Property>();
        if (!Properties.ContainsKey(Property.Prop.FirstCloud))
        {
            var firstProp = new Property()
            {
                EnumName = Property.Prop.FirstCloud,
                Value = 0,
            };
            Properties.Add(Property.Prop.FirstCloud, firstProp);
        }
        if(!Properties.ContainsKey(Property.Prop.HighestScore))
        {
            var scoreProp = new Property()
            {
                EnumName = Property.Prop.HighestScore,
                Value = 0,
            };
            Properties.Add(Property.Prop.HighestScore, scoreProp);
        }
        if (!Properties.ContainsKey(Property.Prop.LongestCloudStreak))
        {
            var streakProp = new Property()
            {
                EnumName = Property.Prop.LongestCloudStreak,
                Value = 0,
            };
            Properties.Add(Property.Prop.LongestCloudStreak, streakProp);
        }
    }
}
