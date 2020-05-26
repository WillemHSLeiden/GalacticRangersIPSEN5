using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class homingProjectile : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private float speed = 3;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void hitEffect() {

    }
}
