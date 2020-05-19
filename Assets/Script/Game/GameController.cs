﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject AudioSourceController;
    // TODO: Maybe load through script
    public GameObject Cloud;
    public GameObject Rock;

    //UI components
    public GameObject GameOverScreen;
    public GameObject ScoreText;
    public GameObject LivesText;
    public GameObject Menu;

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
        acController = PersistentData.Instance.AchievementController;
        AudioSourceController.GetComponent<AudioSource>().volume = PersistentData.Instance.SoundVal;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOver && !PersistentData.Instance.Pause)
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
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause))
        {
            PersistentData.Instance.Pause = Menu.activeSelf ? Menu.activeSelf : !PersistentData.Instance.Pause;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeMenuState();
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
            acController.Points += 100;
            if (!acController.FirstCloud)
            {
                acController.FirstCloud = true;
            }
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
        PersistentData.Instance.Pause = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }
}
