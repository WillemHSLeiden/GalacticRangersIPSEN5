using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreModel
{
    public string username;
    public float timestamp;
    public int score;
    public string gameId;

    public HighscoreModel(string username, int score, string gameId)
    {
        this.username = username;
        this.timestamp = DateTime.Now.Ticks;
        this.score = score;
        this.gameId = gameId;
    }
}
