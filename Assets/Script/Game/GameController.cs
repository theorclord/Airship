using Assets.Script.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AchievementController;

public class GameController : MonoBehaviour
{
    public GameObject AudioSourceController;
    // TODO: Maybe load through script, rather than having it as public properties
    public GameObject Cloud;
    public GameObject Rock;

    //UI components
    public GameObject GameOverScreen;
    public GameObject ScoreText;
    public GameObject LivesText;
    public GameObject Menu;

    //game controller variables
    private float timeCloudLastSpawn;
    private float timeRockLastSpawn;

    public float CurrentSpeedModifier;
    private float CurrentRockSpawnFrequency;
    private float CurrentCloudSpawnFrequency;
    private int lives = Constants.StartingLives;
    private bool HighscoreSaved = false;
    public bool GameOver = false;
    private int currentScore = 0;
    
    public static GameController Instance { get; set; }

    private AchievementController acController;
    void Awake()
    {
        //If we don't currently have a game control...
        if (Instance == null)
            //...set this one to be it...
            Instance = this;
        //...otherwise...
        else if (Instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        LivesText.GetComponent<Text>().text = "Lives: " + lives;
        acController = PersistentData.Instance.AchievementController;
        AudioSourceController.GetComponent<AudioSource>().volume = PersistentData.Instance.SoundVal;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause))
        {
            PersistentData.Instance.Pause = Menu.activeSelf ? Menu.activeSelf : !PersistentData.Instance.Pause;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeMenuState();
        } 
    }

    private void FixedUpdate()
    {
        if (!GameOver && !PersistentData.Instance.Pause)
        {

            var adjustedTime = Time.timeSinceLevelLoad;
            // Speed up the game
            CurrentSpeedModifier = 1 + Mathf.Clamp(Mathf.Floor((adjustedTime) / Constants.SpeedFactor) / 10, 0, Constants.MaxSpeed);
            acController.UpdateProperty(Prop.CurrentSpeed, (int)CurrentSpeedModifier);
            CurrentRockSpawnFrequency = Constants.RockSpawnFrequency / CurrentSpeedModifier;
            CurrentCloudSpawnFrequency = Constants.CloudSpawnFrequency / CurrentSpeedModifier;

            if (adjustedTime - timeCloudLastSpawn > CurrentCloudSpawnFrequency)
            {
                timeCloudLastSpawn = Time.timeSinceLevelLoad;
                Instantiate(Cloud, new Vector3(Constants.SpawnX, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
            }

            if (adjustedTime - timeRockLastSpawn > CurrentRockSpawnFrequency)
            {
                timeRockLastSpawn = Time.timeSinceLevelLoad;
                Instantiate(Rock, new Vector3(Constants.SpawnX, UnityEngine.Random.Range(-10f, 10f)), Quaternion.identity);
            }
        }
    }

    public void ChangeMenuState()
    {
        PersistentData.Instance.Pause = !Menu.activeSelf;
        Menu.SetActive(!Menu.activeSelf);
    }

    public void IncreaseScore()
    {
        if (!GameOver)
        {
            currentScore += 100;
            var newAchievements = new List<Achievement>();
            newAchievements.AddRange(acController.UpdateProperty(Prop.AllTimeScore, PersistentData.Instance.Properties[Prop.AllTimeScore].Value + 100));
            newAchievements.AddRange(acController.UpdateProperty(Prop.CurrentScore, currentScore));
            newAchievements.AddRange(acController.UpdateProperty(Prop.FirstCloud, 1));
            newAchievements.AddRange(acController.UpdateProperty(Prop.CurrentCloudStreak, PersistentData.Instance.Properties[Prop.CurrentCloudStreak].Value + 1));
            // 2020-02-18 TODO: display the new achievements on screen.

            ScoreText.GetComponent<Text>().text = "Score: " + currentScore;
        }
    }

    public void DescreaseLives()
    {
        lives -= 1;
        // call achievement controller
        acController.UpdateProperty(Prop.RocksHit, PersistentData.Instance.Properties[Prop.RocksHit].Value + 1);
        if (lives <= 0 && !GameOver)
        {
            acController.UpdateProperty(Prop.TotalDeaths, PersistentData.Instance.Properties[Prop.TotalDeaths].Value + 1);
            HandleGameOver();
        }
        LivesText.GetComponent<Text>().text = "Lives: " + lives;
    }

    /// <summary>
    /// Ends the game after the player has lost all their lives.
    /// </summary>
    public void HandleGameOver()
    {
        HighscoreSaved = false;
        GameOver = true;
        GameOverScreen.SetActive(true);
        var gameTime = Time.timeSinceLevelLoad;
        HighScoreEntry hsEntry = new HighScoreEntry()
        {
            Date = DateTime.Now,
            Score = currentScore,
            Time = gameTime
        };
        PersistentData.Instance.CurrentHighscoreEntry = hsEntry;
        
        GameOverScreen.transform.GetChild(1).GetComponent<Text>().text = "Score: " + currentScore;
        GameOverScreen.transform.GetChild(2).GetComponent<Text>().text = "Time: " + gameTime;
        // call achievement controller
        acController.UpdateProperty(Prop.CurrentTimeSpent, (int)gameTime);
    }

    public void CloudMissed()
    {
        acController.UpdateProperty(Prop.CurrentCloudStreak, 0);
        acController.UpdateProperty(Prop.CloudsMissed, PersistentData.Instance.Properties[Prop.CloudsMissed].Value + 1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        PersistentData.Instance.Pause = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }

    public void SaveHighscoreData(UnityEngine.UI.Text name)
    {
        if (!HighscoreSaved)
        {
            PersistentData.Instance.CurrentHighscoreEntry.Name = name.text;
            PersistentData.Instance.AddHighscoreEntry(PersistentData.Instance.CurrentHighscoreEntry);
            PersistentData.Instance.SaveHighscoreData();
            HighscoreSaved = true;
        }
    }
}
