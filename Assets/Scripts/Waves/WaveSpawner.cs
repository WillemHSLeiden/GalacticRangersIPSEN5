﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, COUNTING, FINISHED }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeStartSpawning = 1f;
    private SpawnState state = SpawnState.COUNTING;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private FollowPath pathFollower;

    private GameObject playerObj;

    void Start()
    {
        this.playerObj = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        if (pathFollower != null)
        {
            pathFollower.follow();
        }


        despawnEnemy();

        if (timeStartSpawning <= 0)
        {
            if (state != SpawnState.SPAWNING && state != SpawnState.FINISHED)
            {
                //Start spawning wave
                if (waves.Length != nextWave)
                    StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            timeStartSpawning -= Time.deltaTime;
        }
    }

    void timedEvent(Enemy enemy, GameObject go)
    {
        //Ombouwen naar switch
        foreach (TimedEvent te in enemy.enemyEvent)
        {
            switch (te.eventType)
            {
                case EventType.ATTACK:

                    if (go.GetComponent<AttackStrategy>() != null)
                        StartCoroutine(callTimedEvent(te.eventStart, go.GetComponent<AttackStrategy>().startAttacking));
                    break;

/*                case EventType.CHAT:
                  StartCoroutine(callTimedEvent(te.eventStart, enemy.body.GetComponent<BehaviourStrategy>().startChatting));
                  break;
*/
                case EventType.NULL:
                    break;


            }
        }
    }

    IEnumerator callTimedEvent(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();

    }


    void waveCompleted()
    {
        state = SpawnState.COUNTING;
        if (nextWave + 1 > waves.Length - 1 || nextWave == waves.Length)
        {
            state = SpawnState.FINISHED;
        }
        else
        {
            nextWave++;
        }
    }

    bool despawnEnemy()
    {
        bool allEnemiesDespawned = true;
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (pathFollower.pathFinished(i))
            {
                DestroyImmediate(spawnedEnemies[i], true);
                spawnedEnemies[i] = null;
            }
            else
            {
                allEnemiesDespawned = false;
            }
        }

        if (allEnemiesDespawned == true && this.pathFollower != null)
        {
            spawnedEnemies = new List<GameObject>();
            pathFollower.cleanArrays();
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        if (state != SpawnState.FINISHED)
        {
            waveCompleted();
        }

        yield return new WaitForSeconds(_wave.timeTillWaveStarts);

        //Spawn
        for (int i = 0; i < _wave.enemies.Length; i++)
        {   
            if(_wave.enemies[i].type == Enemy.EnemyType.DYNAMIC){
                SpawnEnemy(_wave.enemies[i], _wave.pathCreator);
            }else if(_wave.enemies[i].type == Enemy.EnemyType.STATIC){
                SpawnStaticEnemy(_wave.enemies[i], _wave.spawnPoint);
            }
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        yield break;
    }

    void SpawnEnemy(Enemy enemy, PathCreator path)
    {
        //Spawn enemy
        if(enemy.type == Enemy.EnemyType.DYNAMIC){
            GameObject spawnedEnemy = (GameObject)Instantiate(enemy.body, transform.position, transform.rotation);

            this.setEnemyBehaviours(spawnedEnemy, enemy);

            if(pathFollower == null){
                this.pathFollower = new FollowPath();
            }

            this.pathFollower.addEnemy(spawnedEnemy, enemy, path);
            this.spawnedEnemies.Add(spawnedEnemy);
            this.timedEvent(enemy, spawnedEnemy);
        }
    }

    void SpawnStaticEnemy(Enemy enemy, Transform spawnpoint){
        Instantiate(enemy.body, spawnpoint.position, spawnpoint.rotation);
    }

    void setEnemyBehaviours(GameObject spawnedEnemy, Enemy enemy)
    {
        BehaviourStrategy behaviour = spawnedEnemy.GetComponent<BehaviourStrategy>();
        behaviour.setHealth(enemy.health);
        behaviour.setDamage(enemy.damage);
        behaviour.setSpeed(enemy.speed);
        behaviour.setPlayerObject(this.playerObj.transform);
    }

}
