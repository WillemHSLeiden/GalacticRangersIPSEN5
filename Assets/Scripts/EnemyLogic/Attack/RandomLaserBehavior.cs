using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLaserBehavior : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    public float lifeDuration = 100f;

    private float lifeTimer;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        // Remove the lifetime to see if the bullet should be removed.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
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

    public void setInActive()
    {
        this.gameObject.SetActive(false);
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
        return this.speed;
    }
}
