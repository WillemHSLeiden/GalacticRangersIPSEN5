using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using TMPro;

public class LeaderboardText : MonoBehaviour
{
    TextMeshProUGUI leaderboardText;
    HighscoreService highscoreService = HighscoreService.GetInstance();
    string leaderboardString;

    List<HighscoreModel> models = new List<HighscoreModel>();

    // Start is called before the first frame update
    void Start()
    {
        leaderboardText = gameObject.GetComponent<TextMeshProUGUI>();
        setLeaderboardString();
    }

    public void setLeaderboardString() {
        leaderboardString = "";
        setLeaderboards();
    }

    public void setLeaderboardText() {
        leaderboardText.text = leaderboardString;
    }

    private void Update() {
        setLeaderboardText();
    }

    // Update is called once per frame
    void setLeaderboards()
    {
        highscoreService.GetOverAllHighscores().ContinueWithOnMainThread(task =>
        {
            models = task.Result;

            int i = 0;
            foreach (HighscoreModel model in models) {
                leaderboardString += i + ". " + model.username + "\t-\t" + model.score + "\n";
                i++;
            }

        });
    }
}
