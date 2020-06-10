using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebuloidBehavior : MonoBehaviour, BehaviourStrategy{

    [SerializeField]
    private Material flashMat;

    private float health;

    private float damage;

    private float speed;

    private Transform player;

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Laser") {
            Destroy(collider.gameObject);
            transform.GetChild(0).GetComponent<Renderer>().material = flashMat;
            laserBehavior laser = collider.gameObject.GetComponent<laserBehavior>();
            health -= laser.GetDamage();
        }
        if (health <= 0)
        {
            Invoke("setInActive", 0.05f);
        }
    }
    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void setHealth(float health)
    {
        this.health = health;
    }
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setInActive()
    {
        this.gameObject.SetActive(false);
    }

    public void setPlayerObject(Transform player)
    {
        this.player = player;
    }

    public float getDamage(float damage)
    {
        return this.damage;
    }
}