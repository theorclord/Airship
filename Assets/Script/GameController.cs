using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

  public GameObject Cloud;
  public GameObject Rock;
  public GameObject ScoreText;
  public GameObject LivesText;
  public GameObject GameOverScreen;
  private float timeCloudLastSpawn;
  private float timeRockLastSpawn;

  public float CurrentSpeedModifier;


  private int lives = 3;
  private readonly float speedFactor = 10f;
  //private int rocksSpawned = 2;
  private readonly float spawnX = 25f;
  private float rockSpawnFrequency = 2f;
  private float cloudSpawnFrequency = 1f;
  private readonly float maxSpeed = 20f;

  public bool GameOver = false;
  public float BackgroundSpeed = 2f;
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
    timeCloudLastSpawn = Time.time;
    acController = new AchievementController();
  }

  // Update is called once per frame
  void Update()
  {
    if (!GameOver)
    {
      if (Time.time - timeCloudLastSpawn > cloudSpawnFrequency)
      {
        timeCloudLastSpawn = Time.time;
        Instantiate(Cloud, new Vector3(spawnX, Random.Range(-10f, 10f)), Quaternion.identity);
      }

      if (Time.time - timeRockLastSpawn > rockSpawnFrequency)
      {
        timeRockLastSpawn = Time.time;
        Instantiate(Rock, new Vector3(spawnX, Random.Range(-10f, 10f)), Quaternion.identity);
      }

      // Speed up the game
      CurrentSpeedModifier = 1 + Mathf.Clamp(Mathf.Floor(Time.time / speedFactor) / 10, 0, maxSpeed);
      rockSpawnFrequency = 2f / CurrentSpeedModifier;
      cloudSpawnFrequency = 1f / CurrentSpeedModifier;
    }
  }

  public void IncreaseScore()
  {
    if (!GameOver)
    {
      acController.Points += 100;
      ScoreText.GetComponent<Text>().text = "Score: " + acController.Points;
      acController.StreakCount++;
      // call achievement controller
    }
  }

  public void DescreaseLives()
  {
    lives -= 1;
    if (lives <= 0 && !GameOver)
    {
      HandleGameOver();
    }
    LivesText.GetComponent<Text>().text = "Lives: " + lives;
    // call achievement controller
    acController.InitStreakCount = false;
  }

  public void HandleGameOver()
  {
    GameOver = true;
    GameOverScreen.SetActive(true);
    GameOverScreen.transform.GetChild(1).GetComponent<Text>().text = "Score: " + acController.Points;
    GameOverScreen.transform.GetChild(2).GetComponent<Text>().text = "Time: " + Time.time;
    // call achievement controller
  }

  public void CloudMissed()
  {
    acController.StreakCount = 0;
  }

  public void ReturnToMainMenu()
  {
    SceneManager.LoadScene("Menu");
  }

  public void Retry()
  {
    SceneManager.LoadScene("Main");
  }
}
