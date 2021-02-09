using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static HighScorePanel;

public class HighScoreTimePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadHighScore()
    {
        HighScorePanel.ReloadHighScore(PersistentData.Instance.HighscoreEntriesTime, transform);
    }
}
