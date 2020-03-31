using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

  public GameObject TitleImage;

  public GameObject MainMenu;

  public GameObject OptionsMenu;
  public Slider SoundSlider;

  public GameObject CreditsMenu;

  public GameObject ControlsPanel;
  

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    GetComponent<AudioSource>().volume = PersistentData.Instance().SoundVal;
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

    SoundSlider.value = PersistentData.Instance().SoundVal;
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
  }

  private void SetActiveFalse()
  {
    ControlsPanel.SetActive(false);
    MainMenu.SetActive(false);
    OptionsMenu.SetActive(false);
    TitleImage.SetActive(false);
    CreditsMenu.SetActive(false);
  }

  public void SetSoundLevel()
  {
    //Debug.Log("The current sound value: " + SoundSlider.value);
    PersistentData.Instance().SoundVal = SoundSlider.value;
  }
  public void ExitGame()
  {
    Application.Quit();
  }
}
