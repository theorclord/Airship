using UnityEngine;

/// <summary>
/// Handles the movement of the individual background sprites
/// </summary>
public class BackgroundScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(-GameController.Instance.BackgroundSpeed, 0);
    }
    

    void Update()
    {
        if (GameController.Instance.GameOver)
        {
            rb2d.velocity = new Vector2();
        }
    }
}
