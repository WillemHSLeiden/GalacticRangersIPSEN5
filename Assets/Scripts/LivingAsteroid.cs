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


    public void startAttacking(){
        this.speed = 0;
        anim.SetTrigger("Charge");
    }

    public void shootLaser(){

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
        return this.speed;
    }
}
