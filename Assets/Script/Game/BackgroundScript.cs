using Assets.Script.Data;
using UnityEngine;

/// <summary>
/// Handles the movement of the individual background sprites
/// </summary>
public class BackgroundScript : MonoBehaviour
{
    private BoxCollider2D RepeatCollider;
    private Rigidbody2D Rb2d;
    private float Offset;

    private void Awake()
    {
        RepeatCollider = GetComponent<BoxCollider2D>();
        Offset = RepeatCollider.size.x * 2f; // double to account for the scale of the parent object. The scaling improves the look of the current sprite
    }
    
    void Start()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Rb2d.velocity = new Vector2(-Constants.BackgroundSpeed, 0);
    }
    

    void FixedUpdate()
    {
        if (GameController.Instance.GameOver)
        {
            Rb2d.velocity = new Vector2();
        }
        if (transform.position.x < -Offset * 1.5f) // once the sprite have moved out of the picture and the next one is halv way out as well
        {
            transform.position = (Vector2)transform.position + new Vector2(Offset * 3f, 0); // 3 is the number of background sprites
        }
    }
}
