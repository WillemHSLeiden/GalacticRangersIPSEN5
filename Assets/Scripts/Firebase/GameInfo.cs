using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[Serializable]
public class GameInfo 
{
    public int enemyKills = 0;
    public int shotsFired = 0;
    public int shotsHit = 0;
    public int highestLevelReached = 0;
    public List<PlayerHitInfo> hitMoments = new List<PlayerHitInfo>();
    public List<PowerUpUsage> powerUps = new List<PowerUpUsage>();


    public void PlayerUsedPowerUp(float timestamp, string powerupName)
    {
        powerUps.Add(new PowerUpUsage(timestamp, powerupName));
    }

    public void PlayerGotHit(PlayerHitInfo info)
    {
        hitMoments.Add(info);
    }

    public void PlayerKilledEnemy()
    {
        enemyKills++;
    }

    public void PlayerFiredShot()
    {
        shotsFired++;
    }

    public void PlayerHitShot()
    {
        shotsHit++;
    }

    public void PlayerReachedNextLevel()
    {
        highestLevelReached++;
    }

}
