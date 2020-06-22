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
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private ParticleSystem particle;

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

    public void setInActive(){}
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
}
