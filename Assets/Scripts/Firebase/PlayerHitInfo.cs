using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerHitInfo 
{
    int level = 0;
    string enemyName;
    int damage = 0;
    float timestamp;

    public PlayerHitInfo(int level, string enemyName, int damage)
    {
        this.level = level;
        this.enemyName = enemyName;
        this.damage = damage;
        timestamp = Time.time;
    }
}
