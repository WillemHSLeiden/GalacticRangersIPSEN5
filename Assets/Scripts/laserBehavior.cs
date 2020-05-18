using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBehavior : MonoBehaviour {

    public float speed = 8f;
    public float lifeDuration = 2f;

    private float lifeTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        // Make the bullet move
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        // Remove the lifetime to see if the bullet should be removed.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <=0f){
            Destroy(gameObject);
        }
    }
}
