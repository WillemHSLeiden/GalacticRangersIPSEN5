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
    [SerializeField] private GameObject missle;
    [SerializeField] private float attackSpeed;
    private float attackTimer;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Astroid Rider shoots. pew!");
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
        GameObject enemyMissle = Instantiate(missle, transform.position, transform.localRotation);
        BehaviourStrategy missleBehaviour = enemyMissle.GetComponent<BehaviourStrategy>();
        missleBehaviour.setDamage(damage);
        missleBehaviour.setPlayerObject(player);
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
            Invoke("setInActive", 0.05f);
            GameLogger.GetInstance().PlayerKilledEnemy();
        }
    }
    public void setInActive() { }
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
}
