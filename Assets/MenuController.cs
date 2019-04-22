using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

  public GameObject ControlsPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void StartGame()
  {
    SceneManager.LoadScene("Main");
  }

  public void SwitchControls(bool state)
  {
    ControlsPanel.SetActive(state);
  }
}
