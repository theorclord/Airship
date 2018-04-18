using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

  public GameObject Cloud;
  public GameObject Rock;
  public GameObject ScoreText;
  public GameObject LivesText;
  public GameObject GameOverScreen;
  private float timeCloudLastSpawn;
  private float timeRockLastSpawn;

  private int score;
  private int lives = 3;
  public bool GameOver = false;
  public float BackgroundSpeed = 2f;
  public static GameController Instance
  {
    get; set;
  }

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
  void Start () {
    timeCloudLastSpawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
    if (!GameOver)
    {
      if (Time.time - timeCloudLastSpawn > 1)
      {
        timeCloudLastSpawn = Time.time;
        Instantiate(Cloud, new Vector3(18f, Random.Range(-10f, 10f)), Quaternion.identity);
      }

      if (Time.time - timeRockLastSpawn > 5)
      {
        timeRockLastSpawn = Time.time;
        Instantiate(Rock, new Vector3(18f, Random.Range(-10f, 10f)), Quaternion.identity);
      }
    }
	}

  public void IncreaseScore()
  {
    score += 100;
    ScoreText.GetComponent<Text>().text = "Score: " + score;
  }

  public void DescreaseLives()
  {
    lives -= 1;
    if(lives <= 0)
    {
      GameOver = true;
      GameOverScreen.SetActive(true);
    }
    LivesText.GetComponent<Text>().text = "Lives: " + lives;
  }
}
