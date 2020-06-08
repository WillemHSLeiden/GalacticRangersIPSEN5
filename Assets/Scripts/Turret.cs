using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

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

        var v3 = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0.0f) * 100 * Time.deltaTime;
        //controller.Rotate(v3 * 50 * Time.deltaTime);
        controller.eulerAngles = new Vector3(controller.eulerAngles.x + v3.x, controller.eulerAngles.y + v3.y, 0);

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

    public void ShootLaser()
    {
        Instantiate(laserPrefab, transform.position, transform.localRotation);
    }

    public void Fire()
    {
        if ((Input.GetKeyUp(fire)) && (chargeTimer > initiateChargedLaserTime))
        {
            ShootChargedLaser();

            return;
        }

        if (Input.GetKeyDown(fire)) 
        {
            ShootLaser();
        }

        CountChargeTimer();
        Fire();
    }

}