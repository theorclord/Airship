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
}
