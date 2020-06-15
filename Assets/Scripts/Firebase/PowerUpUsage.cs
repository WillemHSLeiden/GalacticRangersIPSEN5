using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUsage 
{
    float timestamp;
    string powerUpName;

    public PowerUpUsage(float timestamp, string powerUpName)
    {
        this.timestamp = timestamp;
        this.powerUpName = powerUpName;
    }

}
