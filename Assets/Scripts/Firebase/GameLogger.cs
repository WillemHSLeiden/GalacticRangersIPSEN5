using System;
using Firebase;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;

[Serializable]
public class GameLogger
{
    public List<GameLog> logs = new List<GameLog>();
    public GameInfo stats = new GameInfo();

    public void SetTestData()
    {
        LogMessage("Test Test");
        PlayerFiredShot();
        PlayerKilledEnemy();
        PlayerGotHit(new PlayerHitInfo(1, "god", 1));
        PlayerFiredShot();
        PlayerUsedPowerUp("OP");
        LogMessage("Hello?");
        Debug.Log("created TestData");
    }

    public void LogMessage(GameLog log)
    {
        logs.Add(log);
    }

    public void LogMessage(string message)
    {
        LogMessage(new GameLog(DateTime.Now.Ticks, message));
    }

    public void PlayerUsedPowerUp(string powerupName)
    {
        stats.PlayerUsedPowerUp(DateTime.Now.Ticks, powerupName);
    }

    public void PlayerGotHit(PlayerHitInfo info)
    {
        stats.PlayerGotHit(info);
    }

    public void PlayerKilledEnemy()
    {
        stats.PlayerKilledEnemy();
    }

    public void PlayerFiredShot()
    {
        stats.PlayerFiredShot();
    }

    public void PlayerHitShot()
    {
        stats.PlayerHitShot();
    }

    public void PlayerReachedNextLevel()
    {
        stats.PlayerReachedNextLevel();
    }

    //public void SaveDataToFirebase()
    //{
    //    DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("GalaticRangers");
    //    DatabaseReference gameReference = reference.Child("Games").Push();

    //    LogMessage("Welp, would this work?");
    //    Debug.Log(gameLogs[0].text);

    //    string JSONGameLogs = JsonUtility.ToJson(gameLogs);
    //    string JSONGameStats = JsonUtility.ToJson(gameStat);

    //    Debug.Log(JSONGameLogs);
    //    Debug.Log(JSONGameStats);

    //    gameReference.SetRawJsonValueAsync("{\"stats\":" + JSONGameStats + ", \"logs\":" + JSONGameLogs + "}").ContinueWithOnMainThread(task =>
    //    {
    //        if (task.Exception != null)
    //        {
    //            Debug.Log("failed: saving data to database!");
    //        }
    //        else if (task.IsCompleted)
    //        {
    //            Debug.Log("transaction complete.");
    //        }
    //    }); ;
    //}

}
