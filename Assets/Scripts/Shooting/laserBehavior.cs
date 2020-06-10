using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBehavior : MonoBehaviour {

    public float speed = 0.11f;
    public float lifeDuration = 20f;
    public float damage = 1f;
    
    private float lifeTimer;

    // Start is called before the first frame update
    void Start(){
        lifeTimer = lifeDuration;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Make the bullet move
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        // Remove the lifetime to see if the bullet should be removed.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f){
            Destroy(gameObject);
        }
    }
    public float GetDamage() {
        return damage;
    }
}
