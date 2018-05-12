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
    transform.Translate(new Vector3(-1* cloudSpeed * GameController.Instance.CurrentSpeedModifier, 0));
		
	}

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "Airship")
    {
      GameController.Instance.IncreaseScore();
      Destroy(gameObject); // Add points

    }
  }
}
