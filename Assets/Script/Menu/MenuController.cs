using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

  public GameObject MainMenu;
  public GameObject OptionsMenu;
  public GameObject CreditsMenu;
  public Slider SoundSlider;
  public GameObject ControlsPanel;
  public GameObject TitleImage;

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
    MainMenu.SetActive(false);
    OptionsMenu.SetActive(false);
    TitleImage.SetActive(false);
    CreditsMenu.SetActive(true);
  }

  public void Options()
  {
    MainMenu.SetActive(false);
    OptionsMenu.SetActive(true);
    TitleImage.SetActive(false);

    SoundSlider.value = PersistentData.Instance().SoundVal;
  }

  public void MainMenuSet()
  {
    MainMenu.SetActive(true);
    OptionsMenu.SetActive(false);
    TitleImage.SetActive(true);
    CreditsMenu.SetActive(false);
  }

  public void SwitchControls(bool state)
  {
    ControlsPanel.SetActive(state);
    MainMenu.SetActive(!state);
    TitleImage.SetActive(!state);
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
