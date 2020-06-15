using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    private GameObject playerObj;
    private int heal;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Laser")
        {
           Player player = playerObj.GetComponent<Player>();
           player.HealDamage(heal);
        }
        Destroy(collider.gameObject);
        Invoke("SetInactive", 0.05f);
    }

    private void SetInactive()
    {
        this.gameObject.SetActive(false);
    }
}
