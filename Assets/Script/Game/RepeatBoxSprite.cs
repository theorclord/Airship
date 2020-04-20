using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBoxSprite : MonoBehaviour
{

    private BoxCollider2D repeatCollider;
    private float offset;

    private void Awake()
    {
        repeatCollider = GetComponent<BoxCollider2D>();
        offset = repeatCollider.size.x * 2;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -offset * 1.5f)
        {
            transform.position = (Vector2)transform.position + new Vector2(offset * 3f, 0); // 3 is the number of background sprites
        }
    }
}
