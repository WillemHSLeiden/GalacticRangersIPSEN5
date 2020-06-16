using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;



[System.Serializable]
public class Enemy
{

    public enum EnemyType { DYNAMIC, STATIC}
    public string name;
    public float speed;
    public GameObject body;
    public float health;

    public float damage;

    public EnemyType type = EnemyType.DYNAMIC;

    public TimedEvent[] enemyEvent;

}
