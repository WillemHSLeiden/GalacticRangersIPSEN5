using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidRiderBehavior : MonoBehaviour, BehaviourStrategy, AttackStrategy
{
    private float health;
    private float damage;
    private float speed;
    private Transform player;
    [SerializeField] private Material flashMat;
    [SerializeField] private GameObject missle, explosionObject;
    [SerializeField] private float attackSpeed = 10;
    [SerializeField] private Transform spawnPosition, spawnRotation;
    private float attackTimer;

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attacking();
            attackTimer = attackSpeed;

        }
    }
    public void startAttacking()
    {
        attackTimer = attackSpeed;
    }
    public void attacking()
    {
        Instantiate(missle, spawnPosition.position, spawnRotation.rotation);
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
            gameObject.transform.GetChild(0).GetComponent<Renderer>().material = flashMat;
            explode();
            Invoke("setInActive", 0.05f);
            GameLogger.GetInstance().PlayerKilledEnemy();
        }
        if (collider.tag == "ObjectDestroyer") {
            Invoke("setInActive", 0.05f);
        }
    }
    public void setInActive() {
        gameObject.SetActive(false);
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
    public float getHealth()
    {
        return this.health;
    }
    public float getDamage()
    {
        return this.damage;
    }
    public float getSpeed()
    {
        return speed;
    }
    private void explode() {
        Instantiate(explosionObject, transform.position, Quaternion.identity);
    }
}
