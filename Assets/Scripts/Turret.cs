using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour{

    public GameObject laserPrefab;

    public Transform controller;

    // Update is called once per frame
    void Update()
    {
        Quaternion aimDir = controller.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDir, 0.2f);
        
        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Pressed Space so fire?");
            Instantiate(laserPrefab, transform.position, transform.localRotation);
        }

       
    }
}
