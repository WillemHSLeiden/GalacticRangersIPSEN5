using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DatabaseSaveTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            FirebaseCommunicator.GetInstance().StartLogging();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            FirebaseCommunicator.GetInstance().SaveGameData();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            HighscoreService.GetInstance().GetOverAllHighscores().ContinueWithOnMainThread(task =>
            {
                List<HighscoreModel> models = task.Result;

                foreach (HighscoreModel model in models)
                {
                    Debug.Log("test: " + model.score);
                }
            });
        }
    }
}
