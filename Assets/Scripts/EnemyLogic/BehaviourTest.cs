using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTest : MonoBehaviour, BehaviourStrategy
{   
    public void setInActive(){}
    public void setHealth(float health){
        Debug.Log("Health is: "+health);
    }

    public void setDamage(float damage){
        Debug.Log("Damage is: "+damage);
    }

    public void setSpeed(float speed)
    {
        Debug.Log("Speed is: "+ speed);
    }

    public void startChatting()
    {
        throw new System.NotImplementedException();
    }

    public void setPlayerObject(Transform player)
    {
        throw new System.NotImplementedException();
    }
}
