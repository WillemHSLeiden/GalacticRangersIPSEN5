using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour{

    public GameObject laserPrefab;

    public Transform controller;

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = controller.position - transform.position;
        Quaternion aimDir = controller.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDir, 0.2f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(laserPrefab, transform.position, transform.localRotation);
        }

        //Move up or down for test
        if (Input.GetKey(KeyCode.W)) {
            controller.Rotate(-1, 0, 0);
        }

        else if (Input.GetKey(KeyCode.S)){
            controller.Rotate(1, 0, 0);
        }
        //Move left or right for test
        else if (Input.GetKey(KeyCode.A)){
            controller.Rotate(0, -1, 0);
        }

        else if (Input.GetKey(KeyCode.D)){
            controller.Rotate(0, 1, 0);
        }
    }
}
