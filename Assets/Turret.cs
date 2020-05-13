using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform controller;

    // Update is called once per frame
    void Update()
    {
        //Vector3 aimDir = Vector3.RotateTowards(transform.position, controller.position - transform.position, 360, 0);
        Quaternion aimDir = controller.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDir, 0.2f);
    }
}
