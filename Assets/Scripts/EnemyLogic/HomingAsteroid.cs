using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAsteroid : MonoBehaviour, BehaviourStrategy
{

    [SerializeField]
    private Material flashMat;

    private float health;
    private float damage;

    private float speed;

    private Vector3 path;
    private Transform player;

    [SerializeField]
    private GameObject explosionObject;
    void Start(){
        this.createNewPath();
    }
    void Update(){
        this.moveMeteorite();
        transform.Rotate(transform.up * Time.deltaTime * speed * 10);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Laser")
        {
            Destroy(collider.gameObject);
            laserBehavior laser = collider.gameObject.GetComponent<laserBehavior>();
            health -= laser.GetDamage();
        }
        if (health <= 0)
        {
            gameObject.GetComponent<Renderer>().material = flashMat;
            explode();
            Invoke("setInActive", 0.05f);
        }
        if (collider.tag == "ObjectDestroyer") {
            Invoke("setInActive", 0.05f);
        }
    }

    private void createNewPath(){
        GameObject playerObject = GameObject.FindWithTag("Player");
        float newX = playerObject.transform.position.x + Random.Range(-5f, 5f);
        float newY = playerObject.transform.position.y + Random.Range(-5f, 5f);
        float newZ = playerObject.transform.position.z - 20;

        this.path = new Vector3(newX, newY, newZ);
    }


    private void moveMeteorite(){
        transform.position = Vector3.MoveTowards(transform.position, this.path, this.speed*Time.deltaTime);
        this.speed += 5f*Time.deltaTime;
        this.speed = Mathf.Clamp(this.speed, 0, 10);
        if(transform.position == this.path){
            explode();
            Invoke("setInActive", 0.05f);
        }
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void setHealth(float health)
    {
        this.health = health;
    }

    public void setInActive(){
        this.gameObject.SetActive(false);
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
    private void explode() {
        Instantiate(explosionObject, transform.position, Quaternion.identity);
    }
}
