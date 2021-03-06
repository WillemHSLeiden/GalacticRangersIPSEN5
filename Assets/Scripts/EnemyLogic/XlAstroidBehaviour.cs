﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class XlAstroidBehaviour : MonoBehaviour, BehaviourStrategy
{

    public GameObject astroidXlPrefab;

    public GameObject astroidPrefab;

    List<GameObject> spawnedEnemy = new List<GameObject>();
    List<Vector3> endPoints = new List<Vector3>();


     [SerializeField]
    private Material flashMat;

    private float health;

    private float damage;

    private float speed;

    private Transform player;

    private float size;
    // Start is called before the first frame update
    void Start()
    {
        //Make it a random size.
        size = Random.Range (2f, 6f);
        astroidXlPrefab.transform.localScale = new Vector3(size, size, size);
    }

    // // Update is called once per frame
    void Update()
    {Debug.Log(this.spawnedEnemy.Count);
        if(this.spawnedEnemy.Count > 0){

            this.moveMeteorite();
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Laser")
        {
            Destroy(collider.gameObject);
            astroidXlPrefab.GetComponent<Renderer>().material = flashMat;
            laserBehavior laser = collider.gameObject.GetComponent<laserBehavior>();
            health -= laser.GetDamage();
        }
        if (health <= 0)
        {
            splitMeteorite();
            Invoke("setInActive", 0.05f);
        }
        if (collider.tag == "ObjectDestroyer") {
            Invoke("setInActive", 0.05f);
        }
    }

    private void splitMeteorite(){
        for ( int i = 0; i <= size; i++){
            Vector3 currentPos = astroidXlPrefab.transform.position;
            float newX = currentPos.x + Random.Range(-10f, 10f);
            float newY = currentPos.y + Random.Range(-10f, 10f);
            Vector3 newPos = new Vector3(newX, newY, currentPos.z);

            spawnedEnemy.Add( (GameObject) Instantiate(astroidPrefab, newPos, transform.rotation) ) ;
            endPoints.Add(this.createNewPath());
        }
    }

    private Vector3 createNewPath(){
        GameObject playerObject = GameObject.FindWithTag("Player");
        float newX = playerObject.transform.position.x + Random.Range(-10f, 10f);
        float newY = playerObject.transform.position.y + Random.Range(-10f, 10f);
        float newZ = playerObject.transform.position.z + 20;

        Vector3 path = new Vector3(newX, newY, newZ);
        return path;
    }

    private void moveMeteorite(){
        for(int i = 0; i < this.spawnedEnemy.Count; i++){
            // Debug.Log(this.spawnedEnemy[i].transform.position);
            this.spawnedEnemy[i].transform.position = Vector3.MoveTowards(this.spawnedEnemy[i].transform.position, Vector3.zero, 300f*Time.deltaTime);
        }
    }

    public void setInActive(){
        this.astroidXlPrefab.SetActive(false);
    }

    public void setHealth(float health)
    {
        this.health = health;
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setPlayerObject(Transform player)
    {
        this.player = player;
    }
    public float getHealth(){
        return this.health;
    }
    public float getDamage(){
        return this.damage;
    }
    public float getSpeed(){
        return this.speed;
    }
}
