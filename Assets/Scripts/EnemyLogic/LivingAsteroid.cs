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
    [SerializeField] private GameObject _laserPrefab, explosionObject;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Material flashMat;

    private bool keepUpdating;

    void Start(){
        particle.Stop();
    }

    void Update()
    {
        if (keepUpdating)
        {
            transform.LookAt(this.player.transform);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Laser") {
            Destroy(collider.gameObject);
            laserBehavior laser = collider.gameObject.GetComponent<laserBehavior>();
            health -= laser.GetDamage();
        }
        if (health <= 0) {
            gameObject.GetComponent<Renderer>().material = flashMat;
            explode();
            Invoke("setInActive", 0.05f);
            GameLogger.GetInstance().PlayerKilledEnemy();
        }
        if (collider.tag == "ObjectDestroyer") {
            Invoke("setInActive", 0.05f);
        }
    }
    public void startAttacking()
    {
        particle.Play();
        anim.SetTrigger("Charge");
        this.setSpeed(0.1f);
        keepUpdating = true;
        StartCoroutine(startMoving());
    }

    IEnumerator startMoving()
    {
        yield return new WaitForSeconds(5.2f);
        this.setSpeed(15f);
        keepUpdating = false;
    }

    public void shootLaser()
    {
        GameObject laser = Instantiate(_laserPrefab, transform.position, transform.localRotation);
    }

    public void setInActive(){
        gameObject.SetActive(false);
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
    private void explode() {
        Instantiate(explosionObject, transform.position, Quaternion.identity);
    }
}
