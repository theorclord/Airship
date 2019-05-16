using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

  public GameObject MainMenu;
  public GameObject OptionsMenu;
  public Slider SoundSlider;

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
    // show the credit scene
    // hide buttons
    // show credit panel
  }

  public void Options()
  {
    MainMenu.SetActive(false);
    OptionsMenu.SetActive(true);
    SoundSlider.value = PersistentData.Instance().SoundVal;
  }

  public void MainMenuSet()
  {
    MainMenu.SetActive(true);
    OptionsMenu.SetActive(false);
  }

  public void SetSoundLevel()
  {
    Debug.Log("The current sound value: " + SoundSlider.value);
    PersistentData.Instance().SoundVal = SoundSlider.value;
  }
  public void ExitGame()
  {
    Application.Quit();
  }
}
