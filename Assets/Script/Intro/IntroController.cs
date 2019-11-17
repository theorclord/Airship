﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
  private float timeLoadWhenLoaded;
  // Start is called before the first frame update
  void Start()
  {
    timeLoadWhenLoaded = Time.time;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.anyKeyDown)
    {
      SceneManager.LoadScene("Menu");
    }
    if((Time.time - timeLoadWhenLoaded) > 10)
    {
      SceneManager.LoadScene("Menu");
    }
  }
}
