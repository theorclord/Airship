using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  private float speed = 0.15f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKey(KeyCode.D))
    {
      Vector3 movement = new Vector3(1, 0);
      transform.Translate(movement*speed * GameController.Instance.CurrentSpeedModifier);
    }
    if (Input.GetKey(KeyCode.A))
    {
      Vector3 movement = new Vector3(-1, 0);
      transform.Translate(movement * speed * GameController.Instance.CurrentSpeedModifier);
    }
    if (Input.GetKey(KeyCode.W))
    {
      Vector3 movement = new Vector3(0, 1);
      transform.Translate(movement * speed * GameController.Instance.CurrentSpeedModifier);
    }
    if (Input.GetKey(KeyCode.S))
    {
      Vector3 movement = new Vector3(0, -1);
      transform.Translate(movement * speed * GameController.Instance.CurrentSpeedModifier);
    }

    // Clamp the player object, so that it can't leave the camera frame.
    //TODO get size from const class
    Vector3 pospixel = Camera.main.WorldToScreenPoint(transform.position);
    pospixel.x = Mathf.Clamp(pospixel.x, Camera.main.WorldToScreenPoint(Camera.main.ViewportToWorldPoint(Vector3.zero)).x 
      // add half the size of the sprite
      + 40, 
      Camera.main.WorldToScreenPoint(Camera.main.ViewportToWorldPoint(Vector3.right)).x 
      // Subtract half the size of the sprite
      - 40);
    pospixel.y = Mathf.Clamp(pospixel.y, Camera.main.WorldToScreenPoint(Camera.main.ViewportToWorldPoint(Vector3.zero)).y
      // add half the size of the sprite
      +40,
      Camera.main.WorldToScreenPoint(Camera.main.ViewportToWorldPoint(Vector3.up)).y
      // Subtract half the size of the sprite
      - 40);
    transform.position = Camera.main.ScreenToWorldPoint(pospixel);
    
  }
}
