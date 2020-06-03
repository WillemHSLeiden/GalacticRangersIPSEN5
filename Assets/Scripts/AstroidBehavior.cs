using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidBehavior : MonoBehaviour{

    [SerializeField]
    private Material flashMat;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Laser")
        {
            Destroy(collider.gameObject);
            gameObject.GetComponent<Renderer>().material = flashMat;
            Invoke("SetInactive", 0.05f);
        }
    }

    private void SetInactive() {
        this.gameObject.SetActive(false);
    }
}
