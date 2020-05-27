using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidBehavior : MonoBehaviour{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Laser")
        {
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}
