using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public enum Component
    {
        AchievementGrid
    }

    public GameObject TitleImage;

    public GameObject MainMenu;

    public GameObject OptionsMenu;
    public Slider SoundSlider;

    public GameObject CreditsMenu;

    public GameObject ControlsPanel;

    public GameObject AchievementPanel;
    public GameObject HighscorePanel;
    
    void Start()
    {

    }
    
    void Update()
    {
        GetComponent<AudioSource>().volume = PersistentData.Instance.SoundVal;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Credits()
    {
        SetActiveFalse();
        CreditsMenu.SetActive(true);
    }

    public void Options()
    {
        SetActiveFalse();
        OptionsMenu.SetActive(true);

        SoundSlider.value = PersistentData.Instance.SoundVal;
    }

    public void MainMenuSet()
    {
        SetActiveFalse();
        MainMenu.SetActive(true);
        TitleImage.SetActive(true);
    }

    public void SwitchControls()
    {
        SetActiveFalse();
        ControlsPanel.SetActive(true);
    }

    public void ShowAchievements()
    {
        SetActiveFalse();
        AchievementPanel.SetActive(true);
        var containerTransform = AchievementPanel.transform.Find(nameof(Component.AchievementGrid));

        // Clear existing children
        List<GameObject> achieveEntries = new List<GameObject>();
        for (int i = 0; i < containerTransform.childCount; i++)
        {
            achieveEntries.Add(containerTransform.GetChild(i).gameObject);
        }
        foreach (GameObject go in achieveEntries)
        {
            Destroy(go);
        }

        // load the achievements and images from the achievement controllers achievement list
        foreach (var ach in PersistentData.Instance.Achievements)
        {
            var achievementMenuItem = Instantiate(Resources.Load("MenuAchievement"), containerTransform) as GameObject;
            var achievementSprite = Resources.Load<Sprite>(ach.Value.SpriteName);
            var achievementImage = achievementMenuItem.GetComponent<Image>();
            achievementImage.sprite = achievementSprite;

            Color achColor = achievementImage.color;
            achColor.a = ach.Value.UnlockedDate != DateTime.MinValue ? 1f : 0.4f;
            achievementImage.color = achColor;
        }
    }

    private void SetActiveFalse()
    {
        ControlsPanel.SetActive(false);
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        TitleImage.SetActive(false);
        CreditsMenu.SetActive(false);
        AchievementPanel.SetActive(false);
        HighscorePanel.SetActive(false);
    }

    public void SetSoundLevel()
    {
        PersistentData.Instance.SoundVal = SoundSlider.value;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowHighscore()
    {
        SetActiveFalse();
        HighscorePanel.SetActive(true);
        HighscorePanel.GetComponentInChildren<HighScorePanel>().LoadHighScore();
    }
}
