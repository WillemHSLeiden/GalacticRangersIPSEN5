using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseCommunicator
{
    private static FirebaseCommunicator instance;

    public static FirebaseCommunicator GetInstance()
    {
        if (instance == null)
        {
            instance = new FirebaseCommunicator();
        }
        return instance;
    }

    private FirebaseCommunicator()
    {
        StartFirebaseConnection();
    }

    GameLogger gameLogger;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    Boolean isLoggingStarted = false;

    void StartFirebaseConnection()
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
        });

        HighscoreModel model = new HighscoreModel("Ultimate_Gamer", 9999, gameReference.Key);
        HighscoreService.GetInstance().SaveOverAllHighscore(model);

        Debug.Log("Logging Stopped");
        isLoggingStarted = false;
        gameLogger = null;

    }



}
