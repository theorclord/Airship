﻿using Assets.Script.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PersistentData.Instance.Pause)
        {
            transform.Translate(new Vector3(-1 * Constants.RockSpeed * GameController.Instance.CurrentSpeedModifier, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == Constants.PlayerObjectName)
        {
            // Deal damage to airship
            GameController.Instance.DescreaseLives();
            Destroy(gameObject);
        }
    }
}
