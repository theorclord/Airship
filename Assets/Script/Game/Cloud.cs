using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    private float cloudSpeed = 0.02f;
    // Use this for initialization
    void Start()
    {
        GetComponent<AudioSource>().volume = PersistentData.Instance.SoundVal;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PersistentData.Instance.Pause)
        {
            transform.Translate(new Vector3(-1 * cloudSpeed * GameController.Instance.CurrentSpeedModifier, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Airship")
        {
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
            // Add points
            GameController.Instance.IncreaseScore();

            // TODO: render the sprite being smaller, That being render the change from on one sprite loop to another
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            Destroy(gameObject, source.clip.length); 
        }
    }
}
