using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Laser")
        {
            byte laserStage = LaserStage.Instance.getLaserStage();

            if(laserStage == 0){
                LaserStage.Instance.setLaserStage(1);
            }
            if (laserStage == 1){
                LaserStage.Instance.setLaserStage(2);
            }
        }
        Destroy(collider.gameObject);
        Invoke("SetInactive", 0.05f);
    }

    private void SetInactive()
    {
        this.gameObject.SetActive(false);
    }
}
