using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour{

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject chargedLaserPrefab;
    [SerializeField] private KeyCode fire;
    [SerializeField] private int initiateChargedLaserTime;
    [SerializeField] private Transform controller;
    private float chargeTimer;

    void Update()
    {
        Vector3 relativePos = controller.position - transform.position;
        Quaternion aimDir = controller.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, aimDir, 0.2f);

        //Move up or down for test
        if (Input.GetKey(KeyCode.W)) {
            controller.Rotate(-1, 0, 0);
        }

        else if (Input.GetKey(KeyCode.S)){
            controller.Rotate(1, 0, 0);
        }
        //Move left or right for test
        if (Input.GetKey(KeyCode.A)){
            controller.Rotate(0, -1, 0);
        }

        else if (Input.GetKey(KeyCode.D)){
            controller.Rotate(0, 1, 0);
        }

        CountChargeTimer();
        Fire();
    }

    private void CountChargeTimer()
    {
        if (Input.GetKey(fire))
        {
            chargeTimer += Time.deltaTime;
        }
    }

    private void RestartChargedTimer()
    {
        chargeTimer = 0;
    }

    private void ShootChargedLaser()
    {
        Instantiate(chargedLaserPrefab, transform.position, transform.localRotation);
        RestartChargedTimer();
    }

    private void ShootLaser()
    {
        if ((Input.GetKeyUp(KeyCode.Space)) && (chargeTimer < initiateChargedLaserTime))
        {
            Instantiate(laserPrefab, transform.position, transform.localRotation);
            RestartChargedTimer();
        }
    }

    private void Fire()
    {
        if ((Input.GetKeyUp(fire)) && (chargeTimer > initiateChargedLaserTime))
        {
            ShootChargedLaser();

            return;
        }
        ShootLaser();
    }

}
