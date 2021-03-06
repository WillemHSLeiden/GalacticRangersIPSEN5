﻿

using System;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private GameObject laserPrefab, GreenLaserPrefab, PurpleLaserPrefab, chargedLaserPrefab, targetReticle;

    [SerializeField] private KeyCode fire = KeyCode.Space;

    [SerializeField] private float initiateChargedLaserTime = 0.5f;

    private Transform controller;

    [SerializeField] private string lockOnTag = "Enemy";

    [SerializeField] Animator anim;

    private float chargeTimer;

    public GameObject target;

    public bool charging = false;


    private void Start() {
        controller = GameObject.FindGameObjectWithTag("Controller").transform;
    }
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

    private void RayCastLockOn()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.CompareTag(lockOnTag))
            {
                Debug.DrawLine(transform.position, hit.transform.position);
                target = hit.collider.gameObject;
                targetReticle.GetComponent<LockOnReticle>().target = target.transform;
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void CountChargeTimer()
    {

        if (Input.GetKey(fire))
        {
            chargeTimer += Time.deltaTime;
            if (target == null)
            {
                RayCastLockOn();
            }
            if (!charging)
            {
                charging = true;
            }
            if (target != null && !target.activeSelf)
            {
                CancelChargedLaser();
            }
        }

    }

    private void RestartChargedTimer()
    {
        chargeTimer = 0;
    }

    private void ShootChargedLaser()
    {
        SpawnLaser(chargedLaserPrefab);
        CancelChargedLaser();
    }

    private void CancelChargedLaser()
    {
        target = null;
        targetReticle.GetComponent<LockOnReticle>().target = null;
        targetReticle.transform.position = transform.position + new Vector3(-5, -5, -5);
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
    }

    public void ShootLaser()
    {
        //if ((Input.GetKeyUp(fire)) && (chargeTimer < initiateChargedLaserTime))
        //{
            switch (LaserStage.Instance.getLaserStage())
            {
                case 0:
                    SpawnLaser(laserPrefab);
                    break;
                case 1:
                    SpawnLaser(GreenLaserPrefab);
                    break;
                case 2:
                    SpawnLaser(PurpleLaserPrefab);
                    break;
            }
            if (charging)
            {
                charging = false;
                CancelChargedLaser();
            }

        anim.SetTrigger("Fire");
        //}
    }

    private void SpawnLaser(GameObject _laserPrefab)
    {
        GameLogger.GetInstance().PlayerFiredShot();
        RestartChargedTimer();
        GameObject laser = Instantiate(_laserPrefab, transform.position, transform.localRotation);
        laser.GetComponent<LockOn>().target = target;
    }

    private void Fire()
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
    }

}
