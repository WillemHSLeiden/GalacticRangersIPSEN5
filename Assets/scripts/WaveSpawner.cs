using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState{SPAWNING, WAITING, COUNTING, FINISHED}

    [System.Serializable]
    public class Wave
    {
        // Zo kunnen we BV kenbaar maken dat er een boss aankomt.
        public string waveName;
        public Enemy[] enemies;
        //De rate waarop enemies spawnen
        public float rate;
        public Transform[] spawnPoints;
        // public float despawnTimer = 6f;

        public PathCreator pathCreator;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    private SpawnState state = SpawnState.COUNTING;

    private List<GameObject> spawnedEnemies = new List<GameObject>();


    //Kan beter?? 
    private FollowPath pathFollower;

    void Start(){
        pathFollower = new FollowPath();       
        waveCountdown = timeBetweenWaves;
        
    }
    

    void Update(){
        if(pathFollower != null){
            pathFollower.follow();
        }

        if(state == SpawnState.WAITING){
            //Check if enemies are despawned
            if(despawnEnemy(waves[nextWave])){
                //Begin a new wave, previous wave is done.
                waveCompleted();
            }
            return;
        }

        if (waveCountdown <= 0){
            if(state != SpawnState.SPAWNING && state != SpawnState.FINISHED){
                //Start spawning wave
                StartCoroutine( SpawnWave(waves[nextWave]) );
            }
        }else{
            waveCountdown -= Time.deltaTime;
        }
    }

    void timedEvent(Enemy enemy){
        //Ombouwen naar switch
        TimedEvent timedEvent = new TimedEvent();
        if(enemy.enemyEvent.eventType == EventType.ATTACK){
            StartCoroutine(callTimedEvent(enemy.enemyEvent.eventStart, timedEvent.attackEvent ));
        }else{
            StartCoroutine(callTimedEvent(enemy.enemyEvent.eventStart, timedEvent.chatEvent ));
        }
}

    IEnumerator callTimedEvent(float delay, Action action){
        yield return new WaitForSeconds(delay);
        action();
    
    }


    void waveCompleted(){
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length -1  ){
            state = SpawnState.FINISHED;
        }else{
            nextWave++;
        }
    }

    bool despawnEnemy(Wave _wave){
        bool allEnemiesDespawned = true;
        for(int i = 0; i < spawnedEnemies.Count; i++){
            if(pathFollower.pathFinished(i)){
                DestroyImmediate(spawnedEnemies[i], true);
                spawnedEnemies[i] = null;
            }else{
                allEnemiesDespawned = false;
            }
        }
        
        if(allEnemiesDespawned == true){
            spawnedEnemies = new List<GameObject>();
            pathFollower.cleanArrays();
            return true;
        }else{
            return false;
        } 
    }

    IEnumerator SpawnWave(Wave _wave){
        state = SpawnState.SPAWNING;

        //Spawn
        for(int i = 0; i < _wave.enemies.Length; i++){
            SpawnEnemy(_wave.enemies[i], _wave.spawnPoints, _wave.pathCreator);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy( Enemy _enemy, Transform[] _spawnPoint, PathCreator path){
        //Spawn enemy
        // if(_spawnPoint.Length == 0){
        //     Debug.LogError("Geen spawnpoint gegeven");
        // }else{
            // Transform _sp = _spawnPoint[UnityEngine.Random.Range(0, _spawnPoint.Length)];
        GameObject spawnedEnemy = (GameObject) Instantiate(_enemy.body, _enemy.body.transform.position, _enemy.body.transform.rotation);
        pathFollower.addEnemy(spawnedEnemy, _enemy, path);            
        spawnedEnemies.Add(spawnedEnemy);
        timedEvent(_enemy);
        // }
    }

}
