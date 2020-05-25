using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState{SPAWNING, WAITING, COUNTING}

    [System.Serializable]
    public class Wave
    {
        // Zo kunnen we BV kenbaar maken dat er een boss aankomt.
        public string waveName;
        public Enemy[] enemies;
        //De rate waarop enemies spawnen
        public float rate;
        public Transform[] spawnPoints;
        public float despawnTimer = 6f;

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
        waveCountdown = timeBetweenWaves;
        timedEvent();
    }
    

    void Update(){

        if(state == SpawnState.WAITING){
            //Check if enemies are despawned
            if(despawnEnemy(waves[nextWave])){
                //Begin a new wave, previous wave is done.
                timedEvent();
                waveCompleted();
            }else{
                //Prevent spawning a new wave
                if(pathFollower != null){
                    pathFollower.follow();
                }
                return;
            }
        }

        if (waveCountdown <= 0){
            if(state != SpawnState.SPAWNING){
                //Start spawning wave

                StartCoroutine( SpawnWave(waves[nextWave]) );
            }
        }else{
            waveCountdown -= Time.deltaTime;
        }
    }

    void timedEvent(){
        for(int i = 0; i < waves[nextWave].enemies.Length; i++){
            //Ombouwen naar switch
            TimedEvent timedEvent = new TimedEvent();
            if(waves[nextWave].enemies[i].enemyEvent.eventType == EventType.ATTACK){
                StartCoroutine(callTimedEvent(waves[nextWave].enemies[i].enemyEvent.eventStart, timedEvent.attackEvent ));
            }else{
                StartCoroutine(callTimedEvent(waves[nextWave].enemies[i].enemyEvent.eventStart, timedEvent.chatEvent ));
            }
        }
    }

    IEnumerator callTimedEvent(float delay, Action action){
        yield return new WaitForSeconds(delay);
        action();
    
    }


    void waveCompleted(){
        Debug.Log("Wave completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        pathFollower = null;

        if(nextWave + 1 > waves.Length -1  ){
            nextWave = 0;
            Debug.Log("Completed all waves. RESTART...");

        }else{
            nextWave++;
        }
    }

    bool despawnEnemy(Wave _wave){
        _wave.despawnTimer -= Time.deltaTime;
        if(_wave.despawnTimer <= 0){
            _wave.despawnTimer = 6f;
            foreach(UnityEngine.Object enemy in spawnedEnemies){
                DestroyImmediate(enemy, true);
            }
            return true;
        }

        return false;
    }

    IEnumerator SpawnWave(Wave _wave){
        state = SpawnState.SPAWNING;
       
        //Spawn
        for(int i = 0; i < _wave.enemies.Length; i++){
            SpawnEnemy(_wave.enemies[i], _wave.spawnPoints, _wave.pathCreator);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state= SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy( Enemy _enemy, Transform[] _spawnPoint, PathCreator path){
        //Spawn enemy
        if(_spawnPoint.Length == 0){
            Debug.LogError("Geen spawnpoint gegeven");
        }else{
            Transform _sp = _spawnPoint[UnityEngine.Random.Range(0, _spawnPoint.Length)];
            GameObject spawnedEnemy = (GameObject) Instantiate(_enemy.body, _sp.position, _sp.rotation);
            pathFollower = new FollowPath(spawnedEnemy, _enemy, path);            
 
            spawnedEnemies.Add(spawnedEnemy);
        }
    }

}
