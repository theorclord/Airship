using Assets.Script.Data;
using UnityEngine;

public class Border : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != Constants.PlayerObjectName)
        {
            if (collision.gameObject.name == "Cloud(Clone)")
                GameController.Instance.CloudMissed();
            Destroy(collision.gameObject);
        }
    }

}
