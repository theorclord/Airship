using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  public GameObject Cloud;
  private float timeLastSpawn;
	// Use this for initialization
	void Start () {
    timeLastSpawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time- timeLastSpawn > 1)
    {
      timeLastSpawn = Time.time;
      Instantiate(Cloud, new Vector3(5f,Random.Range(-10f,10f)), Quaternion.identity);
    }
	}
}
