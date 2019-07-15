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
    transform.Translate(new Vector3(-1 * rockSpeed* GameController.Instance.CurrentSpeedModifier, 0));
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "Airship")
    {
      GameController.Instance.DescreaseLives();
      Destroy(gameObject); // Deal damage to airship
    }
  }
}
