using Assets.Script.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!PersistentData.Instance.Pause)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Vector3 movement = new Vector3(1, 0);
                transform.Translate(movement * Constants.PlayerSpeed * GameController.Instance.CurrentSpeedModifier);// Mathf.Clamp(GameController.Instance.CurrentSpeedModifier / 3f, 1.0f,10f));
            }
            if (Input.GetKey(KeyCode.A))
            {
                Vector3 movement = new Vector3(-1, 0);
                transform.Translate(movement * Constants.PlayerSpeed * GameController.Instance.CurrentSpeedModifier);// Mathf.Clamp(GameController.Instance.CurrentSpeedModifier / 3f, 1.0f,10f));
            }
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 movement = new Vector3(0, 1);
                transform.Translate(movement * Constants.PlayerSpeed * GameController.Instance.CurrentSpeedModifier);// Mathf.Clamp(GameController.Instance.CurrentSpeedModifier / 3f, 1.0f,10f));
            }
            if (Input.GetKey(KeyCode.S))
            {
                Vector3 movement = new Vector3(0, -1);
                transform.Translate(movement * Constants.PlayerSpeed * GameController.Instance.CurrentSpeedModifier);// Mathf.Clamp(GameController.Instance.CurrentSpeedModifier / 3f, 1.0f,10f));
            }

            // Clamp the player object, so that it can't leave the camera frame.
            Vector3 pospixel = Camera.main.WorldToScreenPoint(transform.position);
            pospixel.x = Mathf.Clamp(pospixel.x, Camera.main.WorldToScreenPoint(Camera.main.ViewportToWorldPoint(Vector3.zero)).x
              // add half the size of the sprite
              + Constants.PlayerSpriteSize,
              Camera.main.WorldToScreenPoint(Camera.main.ViewportToWorldPoint(Vector3.right)).x
              // Subtract half the size of the sprite
              - Constants.PlayerSpriteSize);
            pospixel.y = Mathf.Clamp(pospixel.y, Camera.main.WorldToScreenPoint(Camera.main.ViewportToWorldPoint(Vector3.zero)).y
              // add half the size of the sprite
              + Constants.PlayerSpriteSize,
              Camera.main.WorldToScreenPoint(Camera.main.ViewportToWorldPoint(Vector3.up)).y
              // Subtract half the size of the sprite
              - Constants.PlayerSpriteSize);
            transform.position = Camera.main.ScreenToWorldPoint(pospixel);
        }
    }
}
