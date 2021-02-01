using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePanel : MonoBehaviour
{
    public enum HighscoreEntyComponents
    {
        Name= 0, Score = 1, Time = 2, Date = 3,
    }
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
        for(int i =0;i<PersistentData.Instance.HighScoreList.Count;i++)
        {
            var highscoreEntry = PersistentData.Instance.HighScoreList[i];
            // spawn new highscore prefab
            var hsEntry = Instantiate(Resources.Load("HighscoreEntry"), transform, false) as GameObject;
            hsEntry.transform.GetChild((int)HighscoreEntyComponents.Name).GetComponent<Text>().text = highscoreEntry.Name;
            hsEntry.transform.GetChild((int)HighscoreEntyComponents.Score).GetComponent<Text>().text = highscoreEntry.Score.ToString();
            hsEntry.transform.GetChild((int)HighscoreEntyComponents.Time).GetComponent<Text>().text = highscoreEntry.Time.ToString();
            hsEntry.transform.GetChild((int)HighscoreEntyComponents.Date).GetComponent<Text>().text = highscoreEntry.Date.ToString("yyyy-MM-dd HH:MM");
            hsEntry.transform.position = new Vector3(hsEntry.transform.position.x, hsEntry.transform.position.y - 55*i, hsEntry.transform.position.z);
        }
    }
}
