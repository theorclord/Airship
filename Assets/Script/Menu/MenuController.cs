using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public enum Component
    {
        FirstCloudAchievement
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

        // TODO load the achievements and images from the achievement controllers achievement list
        Image image = AchievementPanel.transform.Find(nameof(Component.FirstCloudAchievement)).GetComponent<Image>();
        Color tempColor = image.color;
        tempColor.a = PersistentData.Instance.Achievements[AchievementController.Achieve.FirstCloud].Validate() ? 1f : 0.4f;
        image.color = tempColor;
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
