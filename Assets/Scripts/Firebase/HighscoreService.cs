using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HighscoreService
{

    private static HighscoreService instance;

    public static HighscoreService GetInstance()
    {
        if (instance == null)
        {
            instance = new HighscoreService();
        }
        return instance;
    }

    private HighscoreService()
    {
        
    }

    public void SaveOverAllHighscore(HighscoreModel model)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("GalaticRangers").Child("Highscore").Child("Overall").Push();

        string JSONHighscore = JsonUtility.ToJson(model);
        Debug.Log("Json: " + JSONHighscore);

        reference.SetRawJsonValueAsync(JSONHighscore).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log("failed: Couldn't save highscore the data to the database.");
            }
            else if (task.IsCompleted)
            {
                Debug.Log("saving completed.");
            }
        });

    }

    public async Task<List<HighscoreModel>> GetOverAllHighscores()
    {
        List<HighscoreModel> models = new List<HighscoreModel>();
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("GalaticRangers").Child("Highscore").Child("Overall");
        await reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            DataSnapshot snapshot = task.Result;
            foreach (DataSnapshot data in snapshot.Children)
            {
                string highscoreJSON = data.GetRawJsonValue();
                HighscoreModel model = JsonUtility.FromJson<HighscoreModel>(highscoreJSON);
                models.Add(model);
            }
        });
        return models;
    }

}