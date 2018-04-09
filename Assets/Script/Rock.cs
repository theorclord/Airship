using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

  private float rockSpeed = 0.05f;
  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    transform.Translate(new Vector3(-1 * rockSpeed, 0));
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "Airship")
    {
      GameObject.Find("GameController").GetComponent<GameController>().DescreaseLives();
      Destroy(gameObject); // Deal damage to airship
    }
  }
}
