using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour {


  private int streakCount;
  public int StreakCount
  {
    get { return streakCount; }
    set
    {
      if(streakCount > LongestStreak)
      {
        LongestStreak = streakCount;
      }
      streakCount = value;
    }
  }
  public bool InitStreakCount { get; set; }
  public float Points { get; set; }


  public int LongestStreak
  {
    get;
    private set;
  }

  // longest dodge
  // dodgeroo

  //no cactch 
  //longest time no sky or rock

  

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}




}
