using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

  private float cloudSpeed = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    transform.Translate(new Vector3(-1* cloudSpeed, 0));
		
	}

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "Airship")
    {
      GameObject.Find("GameController").GetComponent<GameController>().IncreaseScore();
      Destroy(gameObject); // Add points

    }
  }
}
