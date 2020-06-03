using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EventType{ATTACK, CHAT};
[System.Serializable]
public class TimedEvent{
    public EventType eventType;

    public float eventStart;    

    public void attackEvent(){
        //Debug.Log("Attacking");
    }

    public void chatEvent(){
        //Debug.Log("Chatten");
    }

}
