using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  private float speed = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKey(KeyCode.D))
    {
      Vector3 movement = new Vector3(1, 0);
      transform.Translate(movement*speed);
    }
    if (Input.GetKey(KeyCode.A))
    {
      Vector3 movement = new Vector3(-1, 0);
      transform.Translate(movement * speed);
    }
    if (Input.GetKey(KeyCode.W))
    {
      Vector3 movement = new Vector3(0, 1);
      transform.Translate(movement * speed);
    }
    if (Input.GetKey(KeyCode.S))
    {
      Vector3 movement = new Vector3(0, -1);
      transform.Translate(movement * speed);
    }
    transform.position = new Vector2(Mathf.Clamp(transform.position.x, -25f, 25f), Mathf.Clamp(transform.position.y, -7f, 7f));
    
  }
}
