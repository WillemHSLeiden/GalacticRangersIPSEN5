using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision collision)
    {
        //Check to see if the Collider's name is "Chest"
        if (collision.collider.name == "Cube")
        {
            //Output the message
            Debug.Log("Chest is here!");
        }
    }
}
