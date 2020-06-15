using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameLog
{
    public string text;
    public float timestamp;

    public GameLog(float timestamp, string text)
    {
        this.text = text;
        this.timestamp = timestamp;
    }

}
