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

    void Start(){
        // this.setSpeed(3f);
    }
    public void startAttacking(){
        this.setSpeed(0.1f);
        // anim.SetTrigger("Charge");
    }
    

    public void shootLaser(){
        Debug.Log("Schieten");
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
