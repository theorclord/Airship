using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

  public int CloudMissed;
  //public int Streak;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.gameObject.name != "Airship")
    {
      if (collision.gameObject.name == "Cloud(Clone)")
        CloudMissed++;
      Destroy(collision.gameObject);
    }
  }

}
