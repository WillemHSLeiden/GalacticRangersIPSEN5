using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStage : Singleton<LaserStage> {

    //Keeps which type of laser should be shot.

    [SerializeField]
    protected byte laserStage;

    public byte getLaserStage(){
        return laserStage;
    }
    public void setLaserStage(byte laserStage)
    {
        this.laserStage = laserStage;
    }

}
