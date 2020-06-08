using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTest : MonoBehaviour, BehaviourStrategy
{
    public void onTriggerEnter(){}
    public void setInActive(){}
    public void setHealth(float health){
        Debug.Log("Health is: "+health);
    }

    public void setDamage(float health){
        Debug.Log("Damage is: "+health);
    }
}
