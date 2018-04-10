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
  private int lives;
  private bool gameOver;
  // Use this for initialization
  void Start () {
    lives = 3;
    timeCloudLastSpawn = Time.time;
    gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
    if (!gameOver)
    {
      if (Time.time - timeCloudLastSpawn > 1)
      {
        timeCloudLastSpawn = Time.time;
        Instantiate(Cloud, new Vector3(5f, Random.Range(-10f, 10f)), Quaternion.identity);
      }

      if (Time.time - timeRockLastSpawn > 5)
      {
        timeRockLastSpawn = Time.time;
        Instantiate(Rock, new Vector3(5f, Random.Range(-10f, 10f)), Quaternion.identity);
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
      gameOver = true;
      GameOverScreen.SetActive(true);
    }
    LivesText.GetComponent<Text>().text = "Lives: " + lives;
  }
}
