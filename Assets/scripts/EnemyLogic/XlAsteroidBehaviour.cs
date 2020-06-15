using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class XlAsteroidBehaviour : MonoBehaviour, BehaviourStrategy
{

    public GameObject asteroidXlPrefab;

    public GameObject asteroidPrefab;

    List<GameObject> spawnedEnemy = new List<GameObject>();
    List<Vector3> endPoints = new List<Vector3>();


     [SerializeField]
    private Material flashMat;

    private float health;

    private float damage;

    private float speed;

    private float size;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        //Make it a random size.
        size = Random.Range (2f, 6f);
        asteroidXlPrefab.transform.localScale = new Vector3(size, size, size);
    }

    // // Update is called once per frame
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Laser")
        {
            Destroy(collider.gameObject);
            asteroidXlPrefab.GetComponent<Renderer>().material = flashMat;
            laserBehavior laser = collider.gameObject.GetComponent<laserBehavior>();
            health -= laser.GetDamage();
        }
        if (health <= 0)
        {
            splitMeteorite();
            Invoke("setInActive", 0.05f);
        }
    }

    private void splitMeteorite(){
        for ( int i = 0; i <= size; i++){
            Vector3 currentPos = asteroidXlPrefab.transform.position;
            float newX = currentPos.x + Random.Range(-10f, 10f);
            float newY = currentPos.y + Random.Range(-10f, 10f);
            Vector3 newPos = new Vector3(newX, newY, currentPos.z);

            spawnedEnemy.Add( (GameObject) Instantiate(asteroidPrefab, newPos, transform.rotation) ) ;
            this.addBehaviour(spawnedEnemy);
        }
    }

    private void addBehaviour(List<GameObject> spawnedEnemy){
        foreach(GameObject go in spawnedEnemy){
            go.GetComponent<BehaviourStrategy>().setSpeed(this.speed);
            go.GetComponent<BehaviourStrategy>().setHealth(1);
            go.GetComponent<BehaviourStrategy>().setDamage(0);
        }
    }

    public void setInActive(){
        this.asteroidXlPrefab.SetActive(false);
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
