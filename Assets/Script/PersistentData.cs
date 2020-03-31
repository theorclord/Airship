using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PersistentData : MonoBehaviour {

  public float SoundVal { get; set; }

  public AchievementController AchievementController { get; private set; }
    
  public static PersistentData Instance { get; set; }
  void Awake()
  {
        //If we don't currently have a game control...
        if (Instance == null)
        {
            //...set this one to be it...
            Instance = this;
        }
        //...otherwise...
        else if (Instance != this)
        {
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    //Load the soundval from last config file.
    SoundVal = 1;
    AchievementController = new AchievementController();
  }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  private void SaveData()
  {
    StringBuilder data = new StringBuilder();
    string soundValSave = "<soundVal>" + SoundVal + "</soundVal>";
    data.AppendLine(soundValSave);

  }

  private void LoadData()
  {

  }
}
