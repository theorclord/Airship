using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PersistentData : MonoBehaviour {

  public float SoundVal { get; set; }

  private static PersistentData _instance;
  public static PersistentData Instance()
  {
    return _instance;
  }
  void Awake()
  {
    DontDestroyOnLoad(this);
    _instance = this;
    //Load the soundval from last config file.
    SoundVal = 1;
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
