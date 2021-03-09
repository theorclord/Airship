using Assets.Script.Data;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Vector3 CurrentPosition;
    // Use this for initialization
    void Start()
    {
        GetComponent<AudioSource>().volume = PersistentData.Instance.SoundVal;
    }

    void Update()
    {
        if (!PersistentData.Instance.Pause)
        {
            transform.Translate(new Vector3(-1 * Constants.CloudSpeed * GameController.Instance.CurrentSpeedModifier * Time.deltaTime, 0));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (!PersistentData.Instance.Pause)
        //{
        //    transform.Translate(new Vector3(-1 * Constants.CloudSpeed * GameController.Instance.CurrentSpeedModifier*Time.deltaTime, 0));
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == Constants.PlayerObjectName)
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
