using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePanel : MonoBehaviour
{
    public enum HighscoreEntyComponents
    {
        Name = 0, Score = 1, Time = 2, Date = 3,
    }
    public GameObject ConfirmationDialog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetHighscore()
    {
        // Show confirmation box
        ConfirmationDialog.SetActive(true);
    }

    public void ConfirmReset(bool clear)
    {
        // Reset the two highscore lists
        if (clear)
        {
            PersistentData.Instance.ClearHighScore();
        }
        transform.GetComponentInChildren<HighScoreScorePanel>().ReloadHighScore();
        transform.GetComponentInChildren<HighScoreTimePanel>().ReloadHighScore();
        // close the dialog panel
        ConfirmationDialog.SetActive(false);
    }

    public void LoadHighScore()
    {
        transform.GetComponentInChildren<HighScoreScorePanel>().ReloadHighScore();
        transform.GetComponentInChildren<HighScoreTimePanel>().ReloadHighScore();
    }

    public static void ReloadHighScore(List<HighScoreEntry> scoreEntries, Transform containerTransform)
    {
        // Clear existing children
        List<GameObject> highscoreEntries = new List<GameObject>();
        for (int i = 0; i < containerTransform.childCount; i++)
        {
            highscoreEntries.Add(containerTransform.GetChild(i).gameObject);
        }
        foreach (GameObject go in highscoreEntries)
        {
            Destroy(go);
        }

        for (int i = 0; i < scoreEntries.Count; i++)
        {
            var highscoreEntry = scoreEntries[i];
            // spawn new highscore prefab
            var hsEntry = Instantiate(Resources.Load("HighscoreEntry"), containerTransform) as GameObject;
            hsEntry.transform.GetChild((int)HighscoreEntyComponents.Name).GetComponent<Text>().text = highscoreEntry.Name;
            hsEntry.transform.GetChild((int)HighscoreEntyComponents.Score).GetComponent<Text>().text = highscoreEntry.Score.ToString();
            hsEntry.transform.GetChild((int)HighscoreEntyComponents.Time).GetComponent<Text>().text = highscoreEntry.Time.ToString();
            hsEntry.transform.GetChild((int)HighscoreEntyComponents.Date).GetComponent<Text>().text = highscoreEntry.Date.ToString("yyyy-MM-dd HH:MM");
        }
    }
}
