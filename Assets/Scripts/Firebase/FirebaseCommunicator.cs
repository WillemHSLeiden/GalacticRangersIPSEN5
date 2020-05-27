using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseCommunicator : MonoBehaviour
{
    GameLogger gameLogger;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    Boolean isLoggingStarted = false;

    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("Capable of starting firebase Connection...");

            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    public void StartLogging()
    {
        if (!isLoggingStarted)
        {
            Debug.Log("Logging Started");
            isLoggingStarted = true;
            gameLogger = new GameLogger();
            gameLogger.SetTestData();
        }
    }

    public void SaveGameData()
    {
        Debug.Log("Going to try and save the game!");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("GalaticRangers");
        DatabaseReference gameReference = reference.Child("Games").Push();

        string JSONGameLogger = JsonUtility.ToJson(gameLogger);
        Debug.Log("Json: " + JSONGameLogger);

        gameReference.SetRawJsonValueAsync(JSONGameLogger).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log("failed: Couldn't save the data to the database.");
            }
            else if (task.IsCompleted)
            {
                Debug.Log("saving completed.");
            }
        }); ;
        Debug.Log("Logging Stopped");
        isLoggingStarted = false;
        gameLogger = null;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartLogging();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveGameData();
        }
    }

}
