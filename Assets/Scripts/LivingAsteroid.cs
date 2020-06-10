using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingAsteroid : MonoBehaviour, BehaviourStrategy, AttackStrategy
{   
    private float health;
    private float damage;
    private float speed;
    private Transform player;
    public Animator anim;
    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private Material flashMat;

    private bool keepUpdating;

    void Start(){
        // this.setSpeed(3f);
        particle.Stop();

    }

    void Update(){
        if(keepUpdating){
            transform.LookAt(this.player.transform);
        }
    }
    public void startAttacking(){
        particle.Play();
        anim.SetTrigger("Charge");
        this.setSpeed(0.1f);
        keepUpdating = true;
        StartCoroutine(startMoving());        
    }
    
    IEnumerator startMoving(){
        yield return new WaitForSeconds(5.2f);
        this.setSpeed(15f);
        keepUpdating = false;
    }
    public void shootLaser(){
        Debug.Log("Schieten");
    }

    public void setInActive(){
        this.gameObject.SetActive(false);
    }
    public void setHealth(float health){
        this.health = health;
    }
    public void setDamage(float damage){
        this.damage = damage;
    }
    public void setSpeed(float speed){
        this.speed = speed;
    }
    public  void setPlayerObject(Transform player){
        this.player = player;
    }
    public float getHealth(){
        return this.health;
    }
    public float getDamage(){
        return this.damage;
    }
    public float getSpeed(){
        return speed;
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log("Collided");
        if (collider.tag == "Laser") {
            Debug.Log("Collided with Laser");
            Destroy(collider.gameObject);
            laserBehavior laser = collider.gameObject.GetComponent<laserBehavior>();
            health -= laser.GetDamage();
        }
        if (health <= 0) {
            gameObject.GetComponent<Renderer>().material = flashMat;
            Invoke("setInActive", 0.05f);
        }
    }
}
